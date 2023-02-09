using AspNetCoreHero.ToastNotification.Abstractions;
using FilmYonetim.Business.FilmBusinessManagers;
using FilmYonetim.Domain;
using FilmYonetim.Domain.Entities;
using FilmYonetim.Domain.Enums;
using FilmYonetim.Domain.ResultObjectClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmYonetim.Presentation.Controllers
{
    public class FilmController : Controller
    {
        private readonly INotyfService _toastNotification;
        private readonly IFilmBusinessOperations _filmOperations;

        public FilmController(IFilmBusinessOperations filmOperations, INotyfService toastNotification)
        {
            _filmOperations = filmOperations;
            _toastNotification = toastNotification;

        }
        #region Business Views - Actions


        public async Task<IActionResult> List()
        {
            var filmList = await _filmOperations.GetAll();

            return View(filmList);
        }

        public IActionResult Add() {
            return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Film model)
        {
            if (!await TryUpdateModelAsync(model))
            {
                _toastNotification.Error("Kayıt Ekleme Başarısız");
                return View(model);
            }
            await _filmOperations.Add(model);
            _toastNotification.Success("Kayıt Ekleme Başarılı");
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {

            var result = await _filmOperations.GetById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Film model)
        {
            //if (!await TryUpdateModelAsync(model))
            //   return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));
            if (!await TryUpdateModelAsync(model))
            {
                _toastNotification.Error("Kayıt Düzenleme Başarısız");
                return View(model);
            }
            await _filmOperations.Update(model);
            _toastNotification.Success("Kayıt Düzenleme Başarılı");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _filmOperations.Delete(id);

            return RedirectToAction("List");
        }
        #endregion
    }
}

