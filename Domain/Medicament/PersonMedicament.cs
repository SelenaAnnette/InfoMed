namespace Domain.Medicament
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;    

    [Table("PersonMedicament")]
    public class PersonMedicament : DomainBase
    {
        [Required]
        public DateTime RecordingDate { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid MedicamentId { get; set; }
    }
}
