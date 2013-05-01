namespace Domain.Operation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Person;

    [Table("PersonOperation")]
    public class PersonOperation : DomainBase
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid OperationId { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [InverseProperty("PersonOperations")]
        public virtual Operation Operation { get; set; }

        [InverseProperty("PersonOperations")]
        public virtual Person Person { get; set; }
    }
}
