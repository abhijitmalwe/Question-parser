namespace QParser.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PossibleAnswer : BaseEntity
    {
        [Required]
        public long QuestionId { get; set; }

        public string Key { get; set; }

        [DataType(DataType.Html)]
        public string Value { get; set; }
        
        [NotMapped]
        public bool IsCorrect { get; set; } = false;
    }
}