namespace Domain.AllergicReaction
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Person;

    [Table("PersonAllergicReaction")]
    public class PersonAllergicReaction : DomainBase
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid AllergicReactionId { get; set; }

        [Required]
        public DateTime AllergicReactionDate { get; set; }

        [InverseProperty("PersonAllergicReactions")]
        public virtual AllergicReaction AllergicReaction { get; set; }

        [InverseProperty("PersonAllergicReactions")]
        public virtual Person Person { get; set; }
    }
}
