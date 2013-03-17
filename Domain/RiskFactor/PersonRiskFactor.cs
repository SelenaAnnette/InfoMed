namespace Domain.RiskFactor
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;    

    [Table("PersonRiskFactor")]
    public class PersonRiskFactor : DomainBase
    {
        [Required]
        public DateTime RecordingDate { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid RiskFactorId { get; set; }

        [Required]
        public double Value { get; set; }
    }
}
