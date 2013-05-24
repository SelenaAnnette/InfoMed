namespace MobileWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using DataLayer.Persistence.RiskFactor;

    using Domain.RiskFactor;

    public class RiskFactorController : Controller
    {
        private readonly IRiskFactorRepository riskFactorRepository;

        private readonly IPersonRiskFactorRepository personRiskFactorRepository;

        public RiskFactorController(IRiskFactorRepository riskFactorRepository, IPersonRiskFactorRepository personRiskFactorRepository)
        {
            this.riskFactorRepository = riskFactorRepository;
            this.personRiskFactorRepository = personRiskFactorRepository;
        }

        // GET: /RiskFactor/GetRiskFactors
        [HttpGet]
        public JsonResult GetRiskFactors()
        {
            return this.Json(this.riskFactorRepository.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

        // POST: /RiskFactor/AddPersonRiskFactor
        [HttpPost]
        public JsonResult AddPersonRiskFactor(PersonRiskFactor personRiskFactor)
        {
            var isSuccessfully = true;
            var error = string.Empty;

            try
            {
                this.personRiskFactorRepository.CreateOrUpdateEntity(personRiskFactor);
            }
            catch (Exception ex)
            {
                isSuccessfully = false;
                error = ex.Message;
            }

            return this.Json(new { IsSuccessfully = isSuccessfully, Error = error });
        }

        // POST: /RiskFactor/AddPersonRiskFactors
        [HttpPost]
        public JsonResult AddPersonRiskFactors(List<PersonRiskFactor> personRiskFactors)
        {
            var isSuccessfully = true;
            var error = string.Empty;

            foreach (var personRiskFactor in personRiskFactors)
            {
                try
                {
                    this.personRiskFactorRepository.CreateOrUpdateEntity(personRiskFactor);
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
