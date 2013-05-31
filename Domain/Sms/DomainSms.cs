namespace Domain.Sms
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DomainSms")]
    public class DomainSms : DomainBase
    {
        [Required]
        public DateTime SendingDate { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string SenderNumber { get; set; }

        [Required]
        public bool IsRead { get; set; }
    }
}
