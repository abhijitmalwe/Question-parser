namespace QParser.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Course : BaseEntity
    {
        [Required]
        public string ExamName { get; set; }

        public string Description { get; set; }

        public string StudySetting { get; set; }
        public string EngineSetting { get; set; }

        [Required]
        public string Path { get; set; }

        public long? TotalQuestions { get; set; }
        public string License { get; set; }
        public string PublicKey { get; set; }
        public string ActivationCode { get; set; }
        public long? IsLicensed { get; set; } = 0;
        [NotMapped]
        public bool IsValid => (IsLicensed > 0);
    }
}