namespace Domain.LabAnalyze
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("PersonConsultationLabAnalyze")]
    public class PersonConsultationLabAnalyze : DomainBase
    {
        [Required]
        public Guid LabAnalyzeId { get; set; }

        [Required]
        public Guid PersonConsultationId { get; set; }

        [Required]
        public double Value { get; set; }

        [InverseProperty("PersonConsultationLabAnalyzes")]
        public virtual LabAnalyzeType LabAnalyzeType { get; set; }

        [InverseProperty("PersonConsultationLabAnalyzes")]
        public virtual PersonConsultation PersonConsultation { get; set; }
    }
}
