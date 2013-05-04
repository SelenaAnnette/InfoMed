namespace Domain.Diagnosis
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Diagnosises")]
    public class Diagnosis : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Mkb10Code { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("Diagnosis")]
        public virtual ICollection<PersonConsultationDiagnosis> PersonConsultationDiagnosises { get; set; }
    }
}
