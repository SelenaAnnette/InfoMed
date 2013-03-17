namespace Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DomainBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
