namespace Domain.Symptom
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("PersonConsultationSymptom")]
    public class PersonConsultationSymptom : DomainBase
    {                 
        [Required]       
        public Guid ConsultationId { get; set; }

        [Required]
        public Guid SymptomId { get; set; }

        [InverseProperty("PersonConsultationSymptoms")]
        public virtual Symptom Symptom { get; set; }

        [InverseProperty("PersonConsultationSymptoms")]
        public virtual PersonConsultation PersonConsultation { get; set; }
    }
}
