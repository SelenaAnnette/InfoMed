namespace Domain.Person
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.AllergicReaction;
    using Domain.Consultation;
    using Domain.Disease;
    using Domain.Group;
    using Domain.Hospital;
    using Domain.Operation;
    using Domain.RiskFactor;
    using Domain.Symptom;

    [Table("Persons")]
    public class Person : DomainBase
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Sex { get; set; }

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

        public Credentials Credentials { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonGroup> PersonGroups { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonOperation> PersonOperations { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonDisease> PersonDiseases { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonAllergicReaction> PersonAllergicReactions { get; set; }

        [InverseProperty("Doctor")]
        public virtual ICollection<PersonConsultation> ConsultationsAsDoctor { get; set; }

        [InverseProperty("Patient")]
        public virtual ICollection<PersonConsultation> ConsultationsAsPatient { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonHospitalization> PersonHospitalizations { get; set; }
    }

    public enum Sex
    {
        [StringValue("Man")]
        Man,
        [StringValue("Woman")]
        Women
    }
}
