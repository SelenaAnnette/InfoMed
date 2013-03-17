namespace Domain.Person
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PersonContact")]
    public class PersonContact : DomainBase
    {        
        [Required]
        public Guid PersonId { get; set; }
        
        [Required]
        public Guid ContactTypeId { get; set; }

        [Required]
        public string Value { get; set; }

        [InverseProperty("PersonContacts")]
        public virtual ContactType ContactType { get; set; }

        [InverseProperty("PersonContacts")]
        public virtual Person Person { get; set; }
    }
}
