namespace Domain.Consultation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Person;

    [Table("PersonConsultation")]
    public class PersonConsultation : DomainBase
    {
        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid ConsultationTypeId { get; set; }

        [Required]
        public DateTime ConsultationDate { get; set; }

        [InverseProperty("DoctorPersons")]
        public virtual Person Doctor { get; set; }

        [InverseProperty("PatientPersons")]
        public virtual Person Patient { get; set; }

        [InverseProperty("PersonConsultations")]
        public virtual ConsultationType ConsultationType { get; set; }
    }
}
