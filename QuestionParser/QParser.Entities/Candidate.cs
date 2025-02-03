namespace QParser.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Candidate : BaseEntity
    {
        public Candidate()
        {
        }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
    }
}