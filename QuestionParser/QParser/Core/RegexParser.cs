using Microsoft.Extensions.Logging;
using QParser.Admin.Models;
using QParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QParser.Admin.Core
{
    public interface IRegexParser
    {
        //string Content { get; set; }
        //string ExamName { get; set; }

        ExamEntity Parse(FileManager fManager, string content, string examName, bool skipDuplicateCheck);
    }

    public class RegexParser : IRegexParser
    {
        private readonly ILogger<RegexParser> _logger;
        private readonly RegexSettings _regexSettings;
        private bool skipDuplicates = false;
        public RegexParser(ILogger<RegexParser> log, RegexSettings regxSettings)
        {
            _logger = log;
            _regexSettings = regxSettings;
        }

        private List<Question> _questions;

        // public string Content { get; set; }
        public string ExamName { get; set; }


        private FileManager _fileManager;
        // ([\s\S]*?)(?=^question[\s]{0,}: |$(?![\r\n]))  <== This gets the content up to the first QUESTION:

        // (^question?:[\s]{0,}\d{1,}.*)([\s\S]*?)(?=^question[\s]{0,}: |$(?![\r\n]))  <=== This gets the entire question BLOCKS including body, answers etc...

        //  /^question?:[\s]{0,}\d{1,}.*/igm   <==== This is the best one
        // Above can get Question and its number i.e. QUESTION: 78 Note: the last /igm i=ignore case and g=global m=multi-line

        //  /^answer\(s\)?:[\s]{0,}.*/igm <==== Single line
        // /(^answer\(s\)?:[\s]{0,})([\s\S]*?)(?=(^question[\s]{0,1}:|^reference[s]{0,1}[\s]{0,1}:|^explanation[\s]{0,1}:)|$(?![\r\n]))/igm
        // Above gets the answers

        // /(^[A-L][\s]{0,}\.)([\s\S]*?)(?=(^[A-L]\.)|(^^answer\(s\)?:))/igm
        // Get possible list of answer options ie A. answers 1

        // /(^question?:[\s]{0,}\d{1,}.*)([\s\S]*?)(?=^[A-L]\.|^answer\(s\)?:)/igm
        // Get the Question BODY without Answer Options

        // (^reference[s]{0,}:[\s]{0,})([\s\S]*?)(?=(^question[\s]{0,1}:|^explanation[\s]{0,1}:)|$(?![\r\n]))
        // Above gets the reference

        // (^explanation:[\s]{0,})([\s\S]*?)(?=(^question[\s]{0,1}:|^reference[\s]{0,1}:)|$(?![\r\n]))
        // This gets the explanation


        //----------------------PARTS------------------------------------------
        // 1- Get the Whole Part
        //This Get the Each Part
        //((^#([\s]{0,})Topic[:]{0,})([\s\S])*?)((?=^#([\s]{0,}Topic[\s]{0,}))|$(?![\r\n]))
        //

        // 2- Get the Part Title
        // Get the # Topic
        //((^#[\s]{0,}Topic[:]{0,})([\s\S]*?)$(?![\\n]))

        // 3- Get the Intro Text but remove the Title from it using C#
        //((^#[\s]{0,}Topic[:]{0,})([\s\S]*?)(?=QUESTION:[\s]{0,}))


        //----------------------PARTS------------------------------------------

        public ExamEntity Parse(FileManager fManager, string content, string examName, bool skipDuplicateCheck)
        {
            skipDuplicates = skipDuplicateCheck;

            var examParts = new List<ExamPart>();
            ExamPart examPart;

            _logger.LogDebug("Inside Parse() function.");
            _fileManager = fManager;

            var exam = new ExamEntity
            {
                Name = examName.Trim(),
                IntroText = (ParseString(_regexSettings.ExamIntroTextUpToFirstQuestion, content)[0] ?? examName).Trim(),
                TotalQuestions = 0
            };

            //1- Check and get the Parts
            //1.1- Get Part Title
            //1.2- Get Part Body
            //1.3- Get the Questions-Blocks
            var examPartBlocks = ParseString(_regexSettings.PartsBlock, content);
            if (examPartBlocks != null && examPartBlocks.Length > 0)
            {
                foreach (var partBlock in examPartBlocks)
                {
                    examPart = new ExamPart
                    {
                        Title = ParseStringSingleLine(_regexSettings.PartTitle, partBlock).Trim(),
                        BodyContent = ParseStringSingleLine(_regexSettings.PartBodyContent, partBlock).Trim(),
                        Questions = ParseQuestions(exam, partBlock, examName) //Add Questions to Part
                    };
                    examPart.BodyContent = examPart.BodyContent.Replace(examPart.Title, "").Trim();
                    //Add Part to Exam
                    exam.ExamParts.Add(examPart);
                }
                exam.Parts = examPartBlocks.Length;
            }
            else
            {
                //2- If no Parts. Set a Default Part
                //2.1- Set Part Title to Blank
                //2.3- Set Part Body to Blank

                examPart = new ExamPart
                {
                    Title = "",
                    BodyContent = "",
                    Questions = ParseQuestions(exam, content, examName) //Add Questions to Part
                };
                //Add Part to Exam
                exam.ExamParts.Add(examPart);
                exam.Parts = 1;
            }

            return exam;
        }

        List<Question> ParseQuestions(ExamEntity exam, string content, string examName)
        {
            _questions = new List<Question>();
            var qBlocks = ParseString(_regexSettings.QuestionBlockRegex, content);
            var ctr = 1;

            foreach (var qBlock in qBlocks)
            {
                var questions = ParseString(_regexSettings.QuestionRegex, qBlock);
                var questionBodies = ParseString(_regexSettings.QuestionBodyRegex, qBlock);
                var possibleAnswers = ParseString(_regexSettings.PossibleAnswersRegex, qBlock);
                var correctAnswers = ParseString(_regexSettings.CorrectAnswersRegex, qBlock);
                var reference = ParseString(_regexSettings.ReferenceRegex, qBlock);
                var explanation = ParseString(_regexSettings.ExplanationRegex, qBlock);
                var q = new Question
                {
                    Id = 0,
                    Number = (questions != null && questions.Length > 0) ? questions[0].Trim() : "",
                    BodyContent = (questionBodies != null && questionBodies.Length > 0)
                        ? questionBodies[0].Trim().Replace(questions[0].Trim(), "").Trim()
                        : "",
                    PossibleAnswers = ParsePossibleAnswers(possibleAnswers, ctr - 1),
                    CorrectAnswers = (correctAnswers != null && correctAnswers.Length > 0)
                        ? correctAnswers[0].Trim().Replace(_regexSettings.AnswerText, "").Trim()
                        : "",
                    Reference = (reference != null && reference.Length > 0) ? reference[0].Trim() : "",
                    Explanation = (explanation != null && explanation.Length > 0) ? explanation[0].Trim() : ""
                };

                if (ValidateQuestion(q, qBlock))
                {
                    exam.HasError = true;
                }

                _questions.Add(q);
            }

            // This is to check for duplicates questions in the exam. The flag is set inside the appsettings.json file.
            if (!skipDuplicates)
            {
                if (CheckForDuplicate(_questions))
                    exam.HasError = true;
            }

            exam.TotalQuestions += _questions.Count();

            if (_questions != null && exam.Questions != null)
            {
                _questions.ForEach(exam.Questions.Add);
            }
            else
            {
                // Handle the null case appropriately
                throw new InvalidOperationException("Questions collection is not initialized.");
            }

            return _questions;
        }

        private bool CheckForDuplicate(IEnumerable<Question> questions)
        {
            _logger.LogDebug("Inside CheckForDuplicate() function.");

            var flag = false;

            var duplicates = questions.GroupBy(s => s.BodyContent.Trim())
                .Where(q => q.Count() > 1)
                .SelectMany(g => g).ToList();

            if (duplicates.Any())
            {
                flag = true;
                foreach (var dupe in duplicates)
                {
                    _logger.LogDebug($"{dupe}");
                    _fileManager.FileText = $"\nDuplicates: \n {dupe.Number} \n {dupe.BodyContent} \n";
                    _fileManager.WriteAppend();
                }
            }
            _logger.LogDebug($"flag is: {flag}");

            return flag;
        }


        private bool ValidateQuestion(Question q, string qBlock)
        {
            _logger.LogDebug("Inside ValidateQuestion() function.");
            _logger.LogError(qBlock);

            var flag = false;
            if (String.IsNullOrEmpty(q.Number))
            {
                _logger.LogDebug($"q.Number {q.Number} is null");

                flag = true;
            }
            else if (String.IsNullOrEmpty(q.BodyContent))
            {
                _logger.LogDebug($"q.BodyContent {q.BodyContent} is null");

                flag = true;
            }
            else if (q.PossibleAnswers == null)
            {
                _logger.LogDebug($"q.PossibleAnswers is null");
                flag = true;
            }
            else if (q.PossibleAnswers.Count < 1 || q.PossibleAnswers.ToArray()[0] == null)
            {
                _logger.LogDebug($"q.PossibleAnswers is null");
                flag = true;
            }
            else if (!q.PossibleAnswers.Select(x => x.Key.Trim()).ToArray().CharsInOrder())
            {
                _logger.LogDebug($"q.PossibleAnswers is not in order = {q.PossibleAnswers.ToArray()[0]}");
                flag = true;
            }
            else if (String.IsNullOrEmpty(q.CorrectAnswers))
            {
                _logger.LogDebug($"q.CorrectAnswers {q.CorrectAnswers} is null");

                flag = true;
            }
            else if (!String.IsNullOrEmpty(q.CorrectAnswers))
            {
                foreach (string s in q.CorrectAnswers.Split(','))
                {
                    _logger.LogDebug($"{s}");
                    if (q.PossibleAnswers.Any(i => i.Key.Trim() == s.Trim()))
                    {
                        flag = q.PossibleAnswers.All(x => x.Key.Trim() != s.Trim());
                    }
                    else
                    {
                        _logger.LogDebug($"the answers {s} is not in the PossibleAnswers");
                        flag = true;
                    }
                }
            }

            _logger.LogDebug($"flag is: {flag}");

            if (flag)
            {
                _logger.LogDebug($"{qBlock}");
                _fileManager.FileText = qBlock;
                _fileManager.WriteAppend();
                //Write this question to Error Log file
            }

            return flag;
        }

        private PossibleAnswer[] ParsePossibleAnswers(string[] values, int questionId)
        {
            if (values != null && values.Length > 0)
            {
                PossibleAnswer[] items = new PossibleAnswer[values.Length];
                for (var i = 0; i < values.Length; i++)
                {
                    items[i] = new PossibleAnswer { Key = values[i].Trim().Substring(0, 1).Trim(), Value = values[i].Trim(), QuestionId = questionId };
                }

                return items;
            }

            return new PossibleAnswer[1];
        }

        /// <summary>
        /// Takes the Regex Pattern and looks for matches in the content sent to it.
        /// </summary>
        /// <param name="regexPattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private string[] ParseString(string regexPattern, string content)
        {
            var itemRegex = new Regex(regexPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            string[] foundValues = itemRegex.Matches(content).Cast<Match>().ToArray().Select(x => x.Value).ToArray();
            return foundValues;
        }


        private string ParseStringSingleLine(string regexPattern, string content)
        {
            var itemRegex = new Regex(regexPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            string foundValues = itemRegex.Match(content).Value;
            return foundValues;
        }
    }
}