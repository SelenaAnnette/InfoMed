namespace Domain.Symptom
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Symptoms")]
    public class Symptom : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("Symptom")]
        public virtual ICollection<AssignedSymptom> AssignedSymptoms { get; set; }

        [InverseProperty("Symptom")]
        public virtual ICollection<PersonConsultationSymptom> PersonConsultationSymptoms { get; set; }
    }
}
