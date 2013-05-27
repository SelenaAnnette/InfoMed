namespace Domain.Group
{
    using System;    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Person;

    [Table("PersonGroup")]
    public class PersonGroup : DomainBase
    {
        public Guid PersonId { get; set; }

        public Guid GroupId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        [InverseProperty("PersonGroups")]
        public virtual Group Group { get; set; }

        [InverseProperty("PersonGroups")]
        public virtual Person Person { get; set; }
    }
}
