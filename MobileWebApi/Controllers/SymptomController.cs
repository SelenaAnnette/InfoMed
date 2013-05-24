namespace MobileWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using DataLayer.Persistence.Symptom;

    using Domain.Symptom;

    public class SymptomController : Controller
    {
        private readonly ISymptomRepository symptomRepository;

        private readonly IPersonSymptomRepository personSymptomRepository;

        public SymptomController(IPersonSymptomRepository personSymptomRepository)
        {
            this.symptomRepository = symptomRepository;
            this.personSymptomRepository = personSymptomRepository;
        }

        // GET: /Symptom/GetSymptoms
        [HttpGet]
        public JsonResult GetSymptoms()
        {
            return this.Json(this.symptomRepository.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

        // POST: /Symptom/AddPersonSymptom
        [HttpPost]
        public JsonResult AddPersonSymptom(PersonSymptom personSymptom)
        {
            var isSuccessfully = true;
            var error = string.Empty;

            try
            {
                this.personSymptomRepository.CreateOrUpdateEntity(personSymptom);
            }
            catch (Exception ex)
            {
                isSuccessfully = false;
                error = ex.Message;
            }

            return this.Json(new { IsSuccessfully = isSuccessfully, Error = error });
        }

        // POST: /Symptom/AddPersonSymptoms
        [HttpPost]
        public JsonResult AddPersonSymptoms(List<PersonSymptom> personSymptoms)
        {
            var isSuccessfully = true;
            var error = string.Empty;

            foreach (var personSymptom in personSymptoms)
            {
                try
                {
                    this.personSymptomRepository.CreateOrUpdateEntity(personSymptom);
                }
                catch (Exception ex)
                {
                    isSuccessfully = false;
                    error = ex.Message;
                    break;
                }
            }

            return this.Json(new { IsSuccessfully = isSuccessfully, Error = error });
        }
    }
}
