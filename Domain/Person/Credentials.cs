namespace Domain.Person
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Credentials")]
    public class Credentials : DomainBase
    {        
        public Guid PersonId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [InverseProperty("Credentials")]
        public virtual Person Person { get; set; }
    }
}
