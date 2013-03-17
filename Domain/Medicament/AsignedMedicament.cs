namespace Domain.Medicament
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Person;

    [Table("PersonMedicament")]
    public class AsignedMedicament
    {        
        [Key, Column(Order = 1)]
        public Guid PersonId { get; set; }
        
        [Key, Column(Order = 2)]
        public Guid MedicamentId { get; set; }

        [Required]
        public double Dosage { get; set; }

        [Required]
        public string Measure { get; set; }

        [Required]
        public DateTime Frequency { get; set; }

        [Required]
        public bool IsActual { get; set; }

        [InverseProperty("AsignedMedicaments")]
        public virtual Person Person { get; set; }

        [InverseProperty("AsignedMedicaments")]
        public virtual Medicament Medicament { get; set; }
    }
}
