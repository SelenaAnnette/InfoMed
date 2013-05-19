namespace MobileWebApi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using DataLayer.Persistence.Medicament;

    public class MedicamentController : Controller
    {
        private readonly IAssignedMedicamentRepository assignedMedicamentRepository;

        public MedicamentController(IAssignedMedicamentRepository assignedMedicamentRepository)
        {
            this.assignedMedicamentRepository = assignedMedicamentRepository;
        }

        // GET: /Medicament/GetAssignedMedicaments?personId=
        [HttpGet]
        public JsonResult GetAssignedMedicaments(Guid personId)
        {
            var assignedMedicaments =
                this.assignedMedicamentRepository.GetEntitiesByQuery(
                    v => v.PersonConsultation.PatientId == personId).Select(v => new { v.MedicamentId, v.PersonConsultationId, v.MedicamentApplicationWayId, v.Dosage, v.Frequency, v.StartDate, v.FinishDate, v.Id });

            return this.Json(assignedMedicaments, JsonRequestBehavior.AllowGet);
        }
    }
}
