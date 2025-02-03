using System.Linq;

namespace QParser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Question : BaseEntity
    {
        private ICollection<PossibleAnswer> _possibleAnswers;

        public Question()
        {
            PossibleAnswers = new HashSet<PossibleAnswer>();
        }

        [Required]
        public string Number { get; set; }

        [DataType(DataType.Html)]
        public string BodyContent { get; set; }

        public string CorrectAnswers { get; set; }

        [DataType(DataType.Html)]
        public string Reference { get; set; }

        [DataType(DataType.Html)]
        public string Explanation { get; set; }

        public long? ExamId { get; set; }
        public Boolean Marked { get; set; }
        public Boolean RandomSelect { get; set; }

        [Display(Name = "Archive")]
        public bool Archived { get; set; } = false;
       
        [ForeignKey("ExamPart")]
        public long? PartId { get; set; }

        [ForeignKey("PartId")]
        public virtual ExamPart ExamPart { get; set; }

        public virtual ICollection<PossibleAnswer> PossibleAnswers
        {
            get => SetCorrectAnswer();
            set => _possibleAnswers = value;
        }

        private ICollection<PossibleAnswer> SetCorrectAnswer()
        {
            if (_possibleAnswers.Any() && _possibleAnswers != null)
            {
                foreach (PossibleAnswer possibleAnswer in _possibleAnswers)
                {
                    if (possibleAnswer != null)
                    {
                        possibleAnswer.IsCorrect = (CorrectAnswers.Split(',')
                            .Select(str => str.Trim()).ToArray().Contains(possibleAnswer.Key.Trim()));
                    }
                }
            }

            return _possibleAnswers;
        }

        [NotMapped]
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}