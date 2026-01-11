using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asandului_Oana_Maria_Insurance.Models;
using Asandului_Oana_Maria_Insurance.Services;

namespace Asandului_Oana_Maria_Insurance.Controllers
{
    public class ChargesPredictionController : Controller
    {
        private readonly IChargesPredictionService _service;

        public ChargesPredictionController(IChargesPredictionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ChargesPredictionViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ChargesPredictionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var request = new ChargesApiRequest
                {
                    Age = model.Age,
                    Sex = model.Sex,
                    Bmi = model.Bmi,
                    Children = model.Children,
                    Smoker = model.Smoker,
                    Region = model.Region,
                    Charges = 0
                };

                model.PredictedCharges = await _service.PredictAsync(request);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }

            return View(model);
        }
    }
}
