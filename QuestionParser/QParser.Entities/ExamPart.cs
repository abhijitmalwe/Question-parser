namespace QParser.Models
{
    using System.Collections.Generic;
    //using SQLiteNetExtensions.Attributes;
    using System.ComponentModel.DataAnnotations;

    public partial class ExamPart : BaseEntity
    {
        public ExamPart()
        {
            Questions = new HashSet<Question>();
        }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string BodyContent { get; set; }

        public long? ExamId { get; set; }

        [Display(Name = "Archive")]
        public bool Archived { get; set; } = false;

        public virtual ICollection<Question> Questions { get; set; }

    }
}