namespace Domain.Symptom
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Person;    

    [Table("AssignedSymptom")]
    public class AssignedSymptom
    {        
        [Key, Column(Order = 1)]
        public Guid PersonId { get; set; }
        
        [Key, Column(Order = 2)]
        public Guid SymptomId { get; set; }

        [Required]
        public bool IsActual { get; set; }

        [InverseProperty("AssignedSymptoms")]
        public virtual Symptom Symptom { get; set; }

        [InverseProperty("AssignedSymptoms")]
        public virtual Person Person { get; set; }
    }
}
