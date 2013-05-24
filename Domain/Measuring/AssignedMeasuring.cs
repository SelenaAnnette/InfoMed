namespace Domain.Measuring
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("AssignedMeasuring")]
    public class AssignedMeasuring : DomainBase
    {
        [Required]
        public Guid PersonConsultationId { get; set; }

        [Required]
        public Guid MeasuringTypeId { get; set; }

        [Required]
        public double Frequency { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        [InverseProperty("AssignedMeasurings")]
        public virtual PersonConsultation PersonConsultation { get; set; }

        [InverseProperty("AssignedMeasurings")]
        public virtual MeasuringType MeasuringType { get; set; }
    }
}
