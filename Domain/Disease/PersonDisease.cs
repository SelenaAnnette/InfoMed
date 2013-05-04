namespace Domain.Disease
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Person;

    [Table("PersonDisease")]
    public class PersonDisease : DomainBase
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid DiseaseId { get; set; }

        [Required]
        public DateTime DiseaseDate { get; set; }

        [InverseProperty("PersonDiseases")]
        public virtual Disease Disease { get; set; }

        [InverseProperty("PersonDiseases")]
        public virtual Person Person { get; set; }
    }
}
