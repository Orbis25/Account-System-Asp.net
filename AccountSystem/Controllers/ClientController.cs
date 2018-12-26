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
        private readonly IAccountService _accountService;
        private readonly IRequestService _requestService;
        private static int _id;
        public ClientController(IClientService repository ,
            IAccountService accountService ,
            IRequestService requestService)
        {
            _repository = repository;
            _accountService = accountService;
            _requestService = requestService;


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
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
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


        //V2.0
        //---------------------------------------------------------


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
                        UpdateUserNameViewModelAppUs user = new UpdateUserNameViewModelAppUs { Id = model.ApplicationUserId ,
                        UserName = model.Name};
                        _accountService.UpdateUserName(user);
                        Alerts.Type = 5;
                        return RedirectToAction("UserProfile", "Account", new { id = model.ApplicationUserId });
                    }
                }
                else
                {
                    ViewBag.response = 6;
                    return RedirectToAction("UserProfile", "Account", new { id = model.ApplicationUserId });
                }
            }
            catch (Exception)
            {
                ViewBag.response = 6;
                return RedirectToAction("UserProfile", "Account", new { id = model.ApplicationUserId });

                throw;
            }
            return RedirectToAction("UserProfile", "Account", new { id = model.ApplicationUserId });
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            return Json(_repository.Delete(id));
        }

    }
}