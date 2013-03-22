namespace Domain.RiskFactor
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Person;

    [Table("AssignedRiskFactor")]
    public class AssignedRiskFactor
    {        
        [Key, Column(Order = 1)]
        public Guid PersonId { get; set; }
        
        [Key, Column(Order = 2)]
        public Guid RiskFactorId { get; set; }

        [Required]
        public bool IsActual { get; set; }

        [Required]
        public string Measure { get; set; }

        [Required]
        public double Value { get; set; }

        [InverseProperty("AssignedRiskFactors")]
        public virtual Person Person { get; set; }

        [InverseProperty("AssignedRiskFactors")]
        public virtual RiskFactor RiskFactor { get; set; }
    }
}
