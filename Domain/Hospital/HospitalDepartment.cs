namespace Domain.Hospital
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("HospitalDepartments")]
    public class HospitalDepartment : DomainBase
    {
        [Required]
        public Guid HospitalId { get; set; }

        [Required]
        public string Name { get; set; }

        [InverseProperty("HospitalDepartments")]
        public virtual Hospital Hospital { get; set; }

        [InverseProperty("HospitalDepartment")]
        public virtual ICollection<PersonHospitalization> PersonHospitalizations { get; set; }
    }
}
