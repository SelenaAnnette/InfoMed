namespace Domain.Person
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Medicament;
    using Domain.RiskFactor;
    using Domain.Symptom;
    using Domain.Group;

    [Table("Persons")]
    public class Person : DomainBase
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<AssignedSymptom> AssignedSymptoms { get; set; }

        [InverseProperty("FirstPerson")]
        public virtual ICollection<PersonPerson> FirstPersonPersons { get; set; }

        [InverseProperty("SecondPerson")]
        public virtual ICollection<PersonPerson> SecondPersonPersons { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonContact> PersonContacts { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<AssignedRiskFactor> AssignedRiskFactors { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<AssignedMedicament> AssignedMedicaments { get; set; }

        public Credentials Credentials { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonGroup> PersonGroups { get; set; }
    }    
}
