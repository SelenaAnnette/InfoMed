namespace Domain.Consultation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConsultationTypes")]
    public class ConsultationType : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [InverseProperty("ContactType")]
        public virtual ICollection<PersonConsultation> PersonConsultations { get; set; }
    }
}
