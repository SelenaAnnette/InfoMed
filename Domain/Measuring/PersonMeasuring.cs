namespace Domain.Measuring
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PersonMeasuring")]
    public class PersonMeasuring : DomainBase
    {
        [Required]
        public DateTime MeasuringDate { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid MeasuringTypeId { get; set; }

        [Required]
        public double Value { get; set; }
    }
}
