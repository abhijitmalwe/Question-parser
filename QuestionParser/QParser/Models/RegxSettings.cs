using System;
using System.Collections.Generic;
using System.Text;

namespace QParser.Admin.Models
{
  public class RegexSettings
    {
        public string QuestionText { set; get; }
        public string AnswerText { set; get; }
        public string ExamIntroTextUpToFirstQuestion { set; get; }


        public string QuestionBlockRegex { set; get; }
        public string QuestionRegex { set; get; }
        public string QuestionBodyRegex { set; get; }
        public string PossibleAnswersRegex { set; get; }
        public string CorrectAnswersRegex { set; get; }
        public string ReferenceRegex { set; get; }
        public string ExplanationRegex { set; get; }
        public string ImageAttributeTag { set; get; }


        public string PartsBlock { set; get; }
        public string PartTitle { set; get; }
        public string PartBodyContent { set; get; }


    }
}
