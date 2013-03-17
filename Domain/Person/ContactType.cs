namespace Domain.Person
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ContactTypes")]
    public class ContactType : DomainBase
    {
        [Required]
        public string Title { get; set; }

        [InverseProperty("ContactType")]
        public virtual ICollection<PersonContact> PersonContacts { get; set; }
    }
}
