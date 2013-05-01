namespace Domain.Hospital
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Hospitals")]
    public class Hospital : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [InverseProperty("Hospital")]
        public virtual ICollection<HospitalDepartment> HospitalDepartments { get; set; }
    }
}
