namespace Domain.AllergicReaction
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AllergicReactions")]
    public class AllergicReaction : DomainBase 
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [InverseProperty("AllergicReaction")]
        public virtual ICollection<PersonAllergicReaction> PersonAllergicReactions { get; set; }
    }
}
