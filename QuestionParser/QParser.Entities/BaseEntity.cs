namespace QParser.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEntity
    {
        [Key]
        [ReadOnly(true)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ReadOnly(true)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateEntered { get; set; }
    }
}