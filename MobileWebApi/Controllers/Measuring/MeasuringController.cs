using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWebApi.Controllers.Measuring
{
    using System.Web.Script.Serialization;

    using DataLayer.Persistence.Measuring;

    public class MeasuringController : Controller
    {
        public class JsonPersonMeasurings
        {
            public Guid MeasuringTypeId { get; set; }

            public double Value { get; set; }
        }

        private readonly IMeasuringTypeRepository measuringTypeRepository;

        private readonly IPersonMeasuringRepository personMeasuringRepository;

        private readonly PersonMeasuringFactory personMeasuringFactory;

        public MeasuringController(IMeasuringTypeRepository measuringTypeRepository, IPersonMeasuringRepository personMeasuringRepository)
        {
            this.measuringTypeRepository = measuringTypeRepository;
            this.personMeasuringRepository = personMeasuringRepository;
            this.personMeasuringFactory = new PersonMeasuringFactory();
        }

        // GET: /Measuring/GetMeasuringTypes
        [HttpGet]
        public JsonResult GetMeasuringTypes()
        {
            return this.Json(this.measuringTypeRepository.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddPersonMeasurings(Guid personId, DateTime sendingDate, List<JsonPersonMeasurings> measurings)
        {
            var isSuccessfully = true;
            var error = string.Empty;

            foreach (var measuring in measurings)
            {
                try
                {
                    var personMeasuring = this.personMeasuringFactory.Create(
                    Guid.NewGuid(), measuring.MeasuringTypeId, personId, sendingDate, measuring.Value);

                    this.personMeasuringRepository.CreateOrUpdateEntity(personMeasuring);
                }
                catch (Exception ex)
                {
                    isSuccessfully = false;
                    error = ex.Message;
                }
            }

            return this.Json(new { IsSuccessfully = isSuccessfully, Error = error }, JsonRequestBehavior.AllowGet);
        }
    }
}
