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
        private readonly DateTime _dateTime;
        public DebController(IDebService repository)
        {
            _repository = repository;
            _dateTime = DateTime.Now;
        }

        [HttpPost]
        public ActionResult Add(Debs model)
        {
            model.DateTime = _dateTime;
            if (ModelState.IsValid)
            {
                _repository.Add(model);
                 Alerts.Type = 10;
            }
            else
            {
                Alerts.Type = 13;
            }
            return RedirectToAction("Detail","AccountClient", new { id = model.AccountId});
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _repository.Get(id);
            if (model != null)
            {
                var delete = new DeleteDebsViewModel
                {
                    IdAccount = model.AccountId,
                    IdDeb = id
                };
                return View(delete);
            }
            else
            {
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        [HttpGet]
        public ActionResult SuccessDelete(int idAccount , int id )
        {
            if (_repository.Delete(id))
            {
                Alerts.Type = 12;
                return RedirectToAction("Detail","AccountClient",new { id = idAccount });
            }
            else
            {
                return RedirectToAction("PageNotFound", "Error");
            }
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
    }
}