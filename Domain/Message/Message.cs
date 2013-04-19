namespace Domain.Message
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Messages")]
    public class Message : DomainBase
    {
        [Required]
        public Guid SenderPersonId { get; set; }

        [Required]
        public Guid ReceiverPersonId { get; set; }

        [Required]
        public DateTime SendingDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Required]
        public bool IsRead { get; set; }
    }
}
