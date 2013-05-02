namespace Domain.LabAnalyze
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LabAnalyzeTypes")]
    public class LabAnalyzeType : DomainBase
    {
        [Required]
        public string Measure { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("LabAnalyzeType")]
        public virtual ICollection<PersonConsultationLabAnalyze> PersonConsultationLabAnalyzes { get; set; }
    }
}
