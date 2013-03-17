namespace Domain.Symptom
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;     

    [Table("PersonSymptom")]
    public class PersonSymptom : DomainBase
    {                 
        [Required]       
        public DateTime RecordingDate { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid SymptomId { get; set; }
    }
}
