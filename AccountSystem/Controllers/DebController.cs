using Model;
using Model.ViewModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountSystem.Controllers
{
    [Authorize]
    public class DebController : Controller
    {
        private readonly IDebService _repository;
        public DebController(IDebService repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _repository.Get(id);
            if (model != null)
            {
                
                return View(model);
            }
            else
            {
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [HttpPost]
        public ActionResult Update(Debs debs)
        {
            if (debs.Money != 0 )
            {
                var model = _repository.Update(debs);
                if (model)
                {
                    Alerts.Type = 11;
                    return RedirectToAction("Detail", "AccountClient", new { id = debs.AccountId });
                }
                else
                {
                    Alerts.Type = 13;
                    return View(debs);
                }
            }
            else
            {
                Alerts.Type = 13;
                ViewBag.Response = Alerts.Type;
                Alerts.Type = 0;
                return View(debs);

            }
        }

        //-----------v.2.0
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Debs model)
        {
            model.DateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                _repository.Add(model);
                TempData["response"] = "Agregado correctamente";
                TempData["icon"] = "success";
            }
            else
            {
                TempData["response"] = "Lo sentimos!, ha ocurrido un error";
                TempData["icon"] = "error";
            }
            return RedirectToAction("Detail", "AccountClient", new { id = model.AccountId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            return Json(_repository.Delete(id));
        }
    }
}