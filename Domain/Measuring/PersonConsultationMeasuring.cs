namespace Domain.Measuring
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("PersonConsultationMeasuring")]
    public class PersonConsultationMeasuring : DomainBase
    {
        [Required]
        public Guid PersonConsultationId { get; set; }

        [Required]
        public Guid MeasuringTypeId { get; set; }

        [Required]
        public double Value { get; set; }

        [InverseProperty("PersonConsultationMeasurings")]
        public virtual PersonConsultation PersonConsultation { get; set; }

        [InverseProperty("PersonConsultationMeasurings")]
        public virtual MeasuringType MeasuringType { get; set; }
    }
}
