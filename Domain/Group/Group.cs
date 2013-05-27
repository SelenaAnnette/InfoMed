namespace Domain.Group
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Person;

    [Table("Groups")]
    public class Group : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [InverseProperty("Group")]
        public virtual ICollection<PersonGroup> PersonGroups { get; set; }
    }
}
