namespace Domain.Medicament
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Person;

    [Table("AssignedMedicament")]
    public class AssignedMedicament : DomainBase
    {        
        public Guid PersonId { get; set; }

        public Guid MedicamentId { get; set; }

        [Required]
        public double Dosage { get; set; }

        [Required]
        public string Measure { get; set; }

        [Required]
        public double Frequency { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        [InverseProperty("AssignedMedicaments")]
        public virtual Person Person { get; set; }

        [InverseProperty("AssignedMedicaments")]
        public virtual Medicament Medicament { get; set; }
    }
}
