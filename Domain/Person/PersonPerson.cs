namespace Domain.Person
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PersonPerson")]
    public class PersonPerson : DomainBase
    {
        [Required]
        public Guid FirstPersonId { get; set; }

        [Required]
        public Guid SecondPersonId { get; set; }

        [Required]
        public bool IsExist { get; set; }

        [InverseProperty("FirstPersonPersons")]
        public virtual Person FirstPerson { get; set; }

        [InverseProperty("SecondPersonPersons")]
        public virtual Person SecondPerson { get; set; }
    }
}
