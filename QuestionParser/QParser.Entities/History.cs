namespace QParser.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class History : BaseEntity
    {
        [Required]
        public long UserId { get; set; } = 0;

        [Required]
        public long CourseId { get; set; }

        public int CorrectAnswer { get; set; } = 0;
        public int WrongAnswer { get; set; } = 0;
        public int UnAnswered { get; set; } = 0;
        public int PassScore { get; set; }
        public string Duration { get; set; }
        public string QuestionsToReview { get; set; }
        public int TotalQuestions { get; set; } = 0;

        [ForeignKey("UserId")]
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}