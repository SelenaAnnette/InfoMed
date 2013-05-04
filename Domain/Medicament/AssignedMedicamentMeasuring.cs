namespace Domain.Medicament
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Measuring;

    [Table("AssignedMedicamentMeasuring")]
    public class AssignedMedicamentMeasuring : DomainBase
    {        
        public Guid MeasuringTypeId { get; set; }

        public Guid AssignedMedicamentId { get; set; }

        [Required]
        public DateTime TimeIntervar { get; set; }

        [InverseProperty("AssignedMedicamentMeasurings")]
        public virtual AssignedMedicament AssignedMedicament { get; set; }

        [InverseProperty("AssignedMedicamentMeasurings")]
        public virtual MeasuringType MeasuringType { get; set; }
    }
}
