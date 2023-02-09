using FilmYonetim.Business.FilmBusinessManagers;
using FilmYonetim.Business.SalonBusinessManagers;
using FilmYonetim.Business.SeansBusinessManagers;
using FilmYonetim.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmYonetim.Presentation.Controllers
{
    public class SeansController : Controller
    {
        private readonly ISeansBusinessOperations _seansOperations;
        private readonly ISalonBusinessOperations _salonOperations;
        private readonly IFilmBusinessOperations _filmOperations;
        public SeansController(ISeansBusinessOperations seansOperations, ISalonBusinessOperations salonOperations, IFilmBusinessOperations filmOperations)
        {
            _seansOperations = seansOperations;
            _salonOperations = salonOperations;
            _filmOperations = filmOperations;
        }

        #region Business Views - Actions

        public async Task<IActionResult> List()
        {
            var seansList = await _seansOperations.GetAll();

            return View(seansList);
        }

        public async Task<IActionResult> Add()
        {
            var FilmList = await _filmOperations.GetAll();
            ViewBag.Filmler = new SelectList(FilmList, "Id", "FilmAd");
            var SalonList = await _salonOperations.GetAll();
            ViewBag.Salonlar = new SelectList(SalonList, "Id", "SalonAd");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Seans model)
        {
            //if (!await TryUpdateModelAsync(model))
            //    return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));
            int sonuc = await _seansOperations.Add(model);
            if (sonuc == 0)
            {
                return View(model);
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var FilmList = await _filmOperations.GetAll();
            ViewBag.Filmler = new SelectList(FilmList, "Id", "FilmAd");
            var SalonList = await _salonOperations.GetAll();
            ViewBag.Salonlar = new SelectList(SalonList, "Id", "SalonAd");
            var result = await _seansOperations.GetById(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Seans model)
        {
            //if (!await TryUpdateModelAsync(model))
            //   return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));
            var result = await _seansOperations.Update(model);
            var FilmList = await _filmOperations.GetAll();
            ViewBag.Filmler = new SelectList(FilmList, "Id", "FilmAd");
            var SalonList = await _salonOperations.GetAll();
            ViewBag.Salonlar = new SelectList(SalonList, "Id", "SalonAd");
            if (result == 1)

                return View(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _seansOperations.Delete(id);

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> GetByFilm(int id =0)
        {
            var filmList = await _filmOperations.GetAll();
            ViewBag.Seanslar = await _seansOperations.GetByFilm(id);
            return View(filmList);
        }
        [HttpGet]
        public async Task<IActionResult> GetBySalon(int id = 0)
        {
            var SalonList = await _salonOperations.GetAll();
            ViewBag.Seanslar = await _seansOperations.GetBySalon(id);
            return View(SalonList);
        }
        [HttpGet]
        public IActionResult GetByYears()
        {     
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetByYears(Seans s)
        {
            ViewBag.Filmler = await _seansOperations.GetByYillar(s.BaslangicTarih,s.BitisTarih);
            return View();
        }
        #endregion
    }
}