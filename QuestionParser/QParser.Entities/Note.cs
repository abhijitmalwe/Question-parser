namespace QParser.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Note : BaseEntity
    {
        [Required]
        public long QuestionId { get; set; }

        public long? ExamId { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }

    }
}