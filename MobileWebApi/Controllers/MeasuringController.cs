namespace MobileWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using DataLayer.Persistence.Measuring;

    using Domain.Measuring;

    public class MeasuringController : Controller
    {
        private readonly IMeasuringTypeRepository measuringTypeRepository;

        private readonly IPersonMeasuringRepository personMeasuringRepository;

        public MeasuringController(IMeasuringTypeRepository measuringTypeRepository, IPersonMeasuringRepository personMeasuringRepository)
        {
            this.measuringTypeRepository = measuringTypeRepository;
            this.personMeasuringRepository = personMeasuringRepository;
        }

        // GET: /Measuring/GetMeasuringTypes
        [HttpGet]
        public JsonResult GetMeasuringTypes()
        {
            return this.Json(this.measuringTypeRepository.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

        // POST: /Measuring/AddPersonMeasuring
        [HttpPost]
        public JsonResult AddPersonMeasuring(PersonMeasuring personMeasuring)
        {
            var isSuccessfully = true;
            var error = string.Empty;

                try
                {
                    this.personMeasuringRepository.CreateOrUpdateEntity(personMeasuring);
                }
                catch (Exception ex)
                {
                    isSuccessfully = false;
                    error = ex.Message;
                }

            return this.Json(new { IsSuccessfully = isSuccessfully, Error = error });
        }

        // POST: /Measuring/AddPersonMeasurings
        [HttpPost]
        public JsonResult AddPersonMeasurings(List<PersonMeasuring> personMeasurings)
        {
            var isSuccessfully = true;
            var error = string.Empty;

            foreach (var personMeasuring in personMeasurings)
            {
                try
                {
                    this.personMeasuringRepository.CreateOrUpdateEntity(personMeasuring);
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
