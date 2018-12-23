using Model;
using Model.ViewModels;
using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountSystem.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientService _repository;
        private static int _id;
        public ClientController(IClientService repository)
        {
            _repository = repository;
            
        }
        public ActionResult Index(int page = 1)
        {
            try
            {
                ViewBag.response = Alerts.Type;
                ViewBag.Action = "Index";
                ViewBag.Controller = "Client";
                Alerts.Type = 0;
                var model = _repository.GetAllIndex(page);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("PageNotFound", "Error");
                throw;
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _repository.Add(model);
                    if (result)
                    {
                        Alerts.Type = 1;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Alerts.Type = 2;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            ViewBag.response = Alerts.Type;
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if(_repository.NameOfClient(id) != "No hay elementos")
            {
                _id = id;
                ViewBag.PersonName = _repository.NameOfClient(id);
                return View();
            }
            else
            {
                return RedirectToAction("PageNotFound", "Error");
            }

        }

        
        public RedirectToRouteResult SuccessDelete()
        {
            try
            {
                if (_repository.Delete(_id))
                {
                    Alerts.Type = 3;
                }
                else
                {
                    Alerts.Type = 4;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Client model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_repository.Update(model))
                    {
                        Alerts.Type = 5;
                        return RedirectToAction("UserProfile","Account",new { id = model.ApplicationUserId});
                    }
                }
                else
                {
                    ViewBag.response = 6;
                    return View(model);
                }
            }
            catch (Exception)
            {
                ViewBag.response = 6;
                return View(model);

                throw;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Search(string parameter = "" , int page = 1)
        {
            var model = _repository.Search(parameter, page);
            if (model != null)
            {
                ViewBag.pagination = "1";
                ViewBag.parameter = parameter;
                return View("Index", model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}