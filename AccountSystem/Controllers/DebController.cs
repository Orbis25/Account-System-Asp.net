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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Debs model)
        {
            model.DateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (_repository.Add(model))
                {
                    TempData["response"] = "Agregado correctamente";
                    TempData["icon"] = "success";
                    return RedirectToAction("Detail", "AccountClient", new { id = model.AccountId });
                }
            }

            TempData["response"] = "Lo sentimos!, ha ocurrido un error";
                TempData["icon"] = "error";
            
            return RedirectToAction("Detail", "AccountClient", new { id = model.AccountId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            return Json(_repository.Delete(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var model = _repository.Get(id);
            if (model != null)
            {
                return Json(new { Deb = model },JsonRequestBehavior.AllowGet);
            }
            return Json(null,JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Debs debs)
        {
            if (debs.Money > 0)
            {
                if (_repository.Update(debs))
                {
                    TempData["response"] = "Actualizado correctamente";
                    TempData["icon"] = "success";
                    return RedirectToAction("Detail", "AccountClient", new { id = debs.AccountId });
                }
            }
            TempData["response"] = "Lo sentimos, ha ocurrido un error";
            TempData["icon"] = "error";
            return RedirectToAction("Detail", "AccountClient", new { id = debs.AccountId });
        }
    }
}