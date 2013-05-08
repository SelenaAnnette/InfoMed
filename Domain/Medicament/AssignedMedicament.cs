namespace Domain.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("AssignedMedicament")]
    public class AssignedMedicament : DomainBase
    {        
        [Required]
        public Guid MedicamentId { get; set; }

        [Required]
        public Guid PersonConsultationId { get; set; }

        [Required]
        public Guid MedicamentApplicationWayId { get; set; }

        [Required]
        public double Dosage { get; set; }

        [Required]
        public double Frequency { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        [InverseProperty("AssignedMedicaments")]
        public virtual Medicament Medicament { get; set; }

        [InverseProperty("AssignedMedicaments")]
        public virtual PersonConsultation PersonConsultation { get; set; }

        [InverseProperty("AssignedMedicaments")]
        public virtual MedicamentApplicationWay MedicamentApplicationWay { get; set; }

        [InverseProperty("AssignedMedicament")]
        public virtual ICollection<AssignedMedicamentMeasuring> AssignedMedicamentMeasurings { get; set; }
    }
}
