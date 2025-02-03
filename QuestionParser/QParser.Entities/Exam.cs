namespace QParser.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Exam : BaseEntity
    {
        public Exam()
        {
            Questions = new HashSet<Question>();
            ExamParts = new HashSet<ExamPart>();
        }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Html)]
        public string IntroText { get; set; }

        public long CourseId { get; set; }

        public long? TotalQuestions { get; set; }
        public long? Parts { get; set; }

        public virtual ICollection<ExamPart> ExamParts { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

    }
}