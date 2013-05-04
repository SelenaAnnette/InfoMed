namespace Domain.Diagnosis
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Consultation;

    [Table("PersonConsultationDiagnosis")]
    public class PersonConsultationDiagnosis : DomainBase
    {
        [Required]
        public Guid DiagnosisId { get; set; }

        [Required]
        public Guid ConsultationId { get; set; }

        [Required]
        public Guid DiagnosisTypeId { get; set; }

        [Required]
        public Guid ParentPersonConsultationDiagnosisId { get; set; }

        [InverseProperty("ParentPersonConsultationDiagnosis")]
        public virtual ICollection<PersonConsultationDiagnosis> PersonConsultationDiagnosises { get; set; }

        [InverseProperty("PersonConsultationDiagnosises")]
        public virtual PersonConsultationDiagnosis ParentPersonConsultationDiagnosis { get; set; }

        [InverseProperty("PersonConsultationDiagnosises")]
        public virtual DiagnosisType DiagnosisType { get; set; }

        [InverseProperty("PersonConsultationDiagnosises")]
        public virtual Diagnosis Diagnosis { get; set; }

        [InverseProperty("PersonConsultationDiagnosises")]
        public virtual PersonConsultation PersonConsultation { get; set; }
    }
}
