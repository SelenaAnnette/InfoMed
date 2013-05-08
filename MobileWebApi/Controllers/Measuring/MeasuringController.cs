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
        private readonly IMeasuringTypeRepository measuringTypeRepository;

        public MeasuringController(IMeasuringTypeRepository measuringTypeRepository)
        {
            this.measuringTypeRepository = measuringTypeRepository;
        }

        // GET: /Measuring/GetMeasuringTypes
        [HttpGet]
        public JsonResult GetMeasuringTypes()
        {
            return this.Json(this.measuringTypeRepository.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}
