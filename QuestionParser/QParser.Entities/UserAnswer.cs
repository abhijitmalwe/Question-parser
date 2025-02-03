namespace QParser.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class UserAnswer : BaseEntity
    {
        public long UserId { get; set; } = 0;

        [Required]
        public long CourseId { get; set; }

        [Required]
        public long QuestionId { get; set; }

        [Required]
        public string Answers { get; set; }

        public bool Archived { get; set; } = false;
        public long HistoryId { get; set; } = 0;
    }
}