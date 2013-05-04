namespace Domain.Complaint
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("PersonConsultationComplaint")]
    public class PersonConsultationComplaint : DomainBase
    {
        [Required]
        public Guid ConsultationId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("PersonConsultationComplaints")]
        public virtual PersonConsultation PersonConsultation { get; set; }
    }
}
