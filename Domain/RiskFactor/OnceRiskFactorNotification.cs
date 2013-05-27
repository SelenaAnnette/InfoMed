namespace Domain.RiskFactor
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OnceRiskFactorNotifications")]
    public class OnceRiskFactorNotification : DomainBase
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime SendingDate { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
