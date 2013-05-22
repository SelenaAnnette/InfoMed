namespace Domain.Measuring
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Medicament;

    [Table("MeasuringTypes")]
    public class MeasuringType : DomainBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Measuring { get; set; }

        [Required]
        public string Code { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [InverseProperty("MeasuringType")]
        public virtual ICollection<PersonConsultationMeasuring> PersonConsultationMeasurings { get; set; }

        [InverseProperty("MeasuringType")]
        public virtual ICollection<AssignedMedicamentMeasuring> AssignedMedicamentMeasurings { get; set; }

        [InverseProperty("MeasuringType")]
        public virtual ICollection<AssignedMeasuring> AssignedMeasurings { get; set; }
    }
}
