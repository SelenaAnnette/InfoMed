namespace Domain.Group
{
    using System;    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Person;

    [Table("PersonGroup")]
    public class PersonGroup
    {
        [Key, Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Key, Column(Order = 2)]
        public Guid GroupId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        [InverseProperty("PersonGroups")]
        public virtual Group Group { get; set; }

        [InverseProperty("PersonGroups")]
        public virtual Person Person { get; set; }
    }
}
