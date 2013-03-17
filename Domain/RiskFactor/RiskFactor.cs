namespace Domain.RiskFactor
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RiskFactors")]
    public class RiskFactor : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("RiskFactor")]
        public virtual ICollection<AsignedRiskFactor> AsignedRiskFactors { get; set; }
    }
}
