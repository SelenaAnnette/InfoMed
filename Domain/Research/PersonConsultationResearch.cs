namespace Domain.Research
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("PersonConsultationResearch")]
    public class PersonConsultationResearch : DomainBase
    {
        [Required]
        public Guid ResearchId { get; set; }

        [Required]
        public Guid PersonConsultationId { get; set; }

        [Required]
        public string Conclusion { get; set; }

        [InverseProperty("PersonConsultationResearches")]
        public virtual Research Research { get; set; }

        [InverseProperty("PersonConsultationResearches")]
        public virtual PersonConsultation PersonConsultation { get; set; }
    }
}
