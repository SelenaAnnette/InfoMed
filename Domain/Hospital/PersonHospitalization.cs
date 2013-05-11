namespace Domain.Hospital
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Domain.Person;

    [Table("PersonHospitalizations")]
    public class PersonHospitalization : DomainBase
    {
        [Required]
        public Guid HospitalDepartmentId { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public DateTime HospitalizationDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        [Required]
        public string HospitalizationsReason { get; set; }

        [InverseProperty("PersonHospitalizations")]
        public virtual HospitalDepartment HospitalDepartment { get; set; }

        [InverseProperty("PersonHospitalizations")]
        public virtual Person Person { get; set; }
    }
}
