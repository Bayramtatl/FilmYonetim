using FilmYonetim.Business.FilmBusinessManagers;
using FilmYonetim.Business.SalonBusinessManagers;
using FilmYonetim.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmYonetim.Presentation.Controllers
{
    public class SalonController : Controller
    {
        private readonly ISalonBusinessOperations _salonOperations;
        public SalonController(ISalonBusinessOperations salonOperations)
        {
            _salonOperations = salonOperations;

        }
        #region Business Views - Actions


        public async Task<IActionResult> List()
        {
            var salonList = await _salonOperations.GetAll();

            return View(salonList);
        }

        public IActionResult Add() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Salon model)
        {
            //if (!await TryUpdateModelAsync(model))
            //    return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));
            int sonuc = await _salonOperations.Add(model);
            if (sonuc == 0)
            {
                return View(model);
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _salonOperations.GetById(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Salon model)
        {
            //if (!await TryUpdateModelAsync(model))
            //   return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));
            var result = await _salonOperations.Update(model);
            if (result == 1)
                return View(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _salonOperations.Delete(id);

            return RedirectToAction("List");
        }
        #endregion


    }
}
