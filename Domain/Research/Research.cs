namespace Domain.Research
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Researches")]
    public class Research : DomainBase
    {
        [Required]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("Research")]
        public virtual ICollection<PersonConsultationResearch> PersonConsultationResearches { get; set; }
    }
}
