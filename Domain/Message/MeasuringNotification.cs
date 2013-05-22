namespace Domain.Message
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MeasuringNotifications")]
    public class MeasuringNotification : DomainBase
    {
        public Guid AssignedMeasuringId { get; set; }

        public Guid MeasuringTypeId { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public DateTime SendingDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        
        public DateTime? ExecutedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
