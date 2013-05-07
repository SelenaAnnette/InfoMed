namespace Domain.Medicament
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MedicamentForms")]
    public class MedicamentForm : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Measuring { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("MedicamentForm")]
        public virtual ICollection<Medicament> Medicaments { get; set; }
    }
}
