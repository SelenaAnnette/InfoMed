namespace Domain.Operation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Operations")]
    public class Operation : DomainBase
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [InverseProperty("Operation")]
        public virtual ICollection<PersonOperation> PersonOperations { get; set; }
    }
}
