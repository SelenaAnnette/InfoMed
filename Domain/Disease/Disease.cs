namespace Domain.Disease
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Diseases")]
    public class Disease : DomainBase
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [InverseProperty("Disease")]
        public virtual ICollection<PersonDisease> PersonDiseases { get; set; }
    }
}
