namespace Domain.Consultation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Complaint;
    using Domain.Diagnosis;
    using Domain.LabAnalyze;
    using Domain.Measuring;
    using Domain.Medicament;
    using Domain.Person;
    using Domain.Research;
    using Domain.Symptom;

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

        [InverseProperty("ConsultationsAsDoctor")]
        public virtual Person Doctor { get; set; }

        [InverseProperty("ConsultationsAsPatient")]
        public virtual Person Patient { get; set; }

        [InverseProperty("PersonConsultations")]
        public virtual ConsultationType ConsultationType { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<PersonConsultationResearch> PersonConsultationResearches { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<PersonConsultationLabAnalyze> PersonConsultationLabAnalyzes { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<PersonConsultationComplaint> PersonConsultationComplaints { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<PersonConsultationSymptom> PersonConsultationSymptoms { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<PersonConsultationMeasuring> PersonConsultationMeasurings { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<PersonConsultationDiagnosis> PersonConsultationDiagnosises { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<AssignedMedicament> AssignedMedicaments { get; set; }

        [InverseProperty("PersonConsultation")]
        public virtual ICollection<AssignedMeasuring> AssignedMeasurings { get; set; }
    }
}
