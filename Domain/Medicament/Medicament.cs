namespace Domain.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Medicaments")]
    public class Medicament : DomainBase
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid MedicamentFormId { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("Medicament")]
        public virtual ICollection<AssignedMedicament> AssignedMedicaments { get; set; }

        [InverseProperty("Medicaments")]
        public virtual MedicamentForm MedicamentForm { get; set; }
    }
}
