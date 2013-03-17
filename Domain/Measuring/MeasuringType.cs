﻿namespace Domain.Measuring
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MeasuringTypes")]
    public class MeasuringType : DomainBase
    {
        [Required]
        public string Title { get; set; }
    }
}