namespace Domain.Diagnosis
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DiagnosisTypes")]
    public class DiagnosisType : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("DiagnosisType")]
        public virtual ICollection<PersonConsultationDiagnosis> PersonConsultationDiagnosises { get; set; }
    }
}
