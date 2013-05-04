namespace Domain.Medicament
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MedicamentApplicationWays")]
    public class MedicamentApplicationWay : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("MedicamentApplicationWay")]
        public virtual ICollection<AssignedMedicament> AssignedMedicaments { get; set; }
    }
}
