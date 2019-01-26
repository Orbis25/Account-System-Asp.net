using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.AspNet.Identity;
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
    public class AccountClientController : Controller
    {
        private readonly IAccountClientService _repository;
        private readonly IClientService _repositoryClient;
        private readonly IRequestService _requestService;
        private readonly IDebService _debService;
        public AccountClientController(IAccountClientService repository
            , IClientService repositoryClient
            , IRequestService requestService,
            IDebService debService)
        {
            _repository = repository;
            _repositoryClient = repositoryClient;
            _requestService = requestService;
            _debService = debService;
        }

        [HttpGet]
        public ActionResult PayOff(int id)
        {
            if (_repository.Get(id)!=null)
            {
                if (_repository.PayOff(id))
                {
                }

                return RedirectToAction("Detail", new { id });
            }
            else
            {
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        public ActionResult GeneratePdf(int id)
        {
            string pathReport = Server.MapPath("~/Report/Report.rpt");
            string PathPdf = Server.MapPath("~/Report/Pdf/archivo.pdf");


            if (_repository.Get(id) != null)
            {
                if (_repository.GetWithClientAndDebs(id).TotalOfRegister != 0)
                {
                    if (_repository.Report(id, pathReport, PathPdf))
                    {
                        return File(PathPdf, "application/pdf", "archivo.pdf");
                    }
                    else
                    {
                        return RedirectToAction("PageNotFound", "Error");
                    }
                }
                else
                {
                    //Alerts.Type = 17;
                    return RedirectToAction("Detail", new { id });
                }
            }
            else
            {
                return RedirectToAction("PageNotFound", "Error");
            }
        }

        //v2.0-------------------------------------------------
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Account model)
        {
           model.CreatedAt = DateTime.Now;
           if (ModelState.IsValid)
           {
                if (!_repository.VerifyRequestAndClient(model.RequestId,model.ClientId))
                {
                    if (_repository.Add(model))
                    {
                        TempData["response"] = "Cuenta creada con exito";
                        TempData["icon"] = "success";
                    }
                    else
                    {
                        TempData["response"] = "Lo sentimos, no se ha creado la cuenta";
                        TempData["icon"] = "error";
                    }
                }
                else
                {
                    TempData["response"] = "Este cliente tiene una cuenta con esta solicitud, porfavor solicite una nueva cuenta";
                    TempData["icon"] = "error";
                }
            }
            else
            {
                TempData["response"] = "Lo sentimos, no se ha creado la cuenta";
                TempData["icon"] = "error";
            }
            return RedirectToAction("Index", "AccountClient");
        }

        [HttpGet]
        public ViewResult Index(int page = 1)
        {
            var model = _repository.GetAllByPage(page);
            if (model != null)
            {
                ViewBag.Clients = _repositoryClient.GetAll();
                ViewBag.Requests = _requestService.GetAll();
                if (TempData["response"] != null)
                {
                    ViewBag.Alert = TempData["response"].ToString();
                    ViewBag.Icon = TempData["Icon"].ToString();
                }
            }
            return View(model);
        }

        [HttpGet]
        public ViewResult MyAccounts(int page = 1)
        {
            return View("Index",_repository.MyAccounts(User.Identity.GetUserId(),page));
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            return Json(_repository.Delete(id));
        }
        
        [HttpGet]
        public JsonResult GetById(int id)
        {
            //colocamos allowGet para permitir revelar informacion
            return Json(_repository.Get(id), JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Account model)
        {
            var result = _repository.Update(model);
            if (result)
            {
                TempData["response"] = "Cuenta actualizada";
                TempData["Icon"] = "success";
            }
            else
            {
                TempData["response"] = "Lo sentimos, no se ha actualizado la cuenta";
                TempData["Icon"] = "error";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string parameter = "", int page = 1)
        {
            ViewBag.pagination = "1";
            ViewBag.parameter = parameter;
            ViewBag.Clients = _repositoryClient.GetAll();
            ViewBag.Requests = _requestService.GetAll();
            return View("Index", _repository.Search(parameter, page));            
        }

        [HttpGet]
        public ActionResult Detail(int id, int page = 1)
        {
            page = page == 0 ? 1 : page;
            if (User.IsInRole("Admin"))
            {
                var model = _repository.GetWithClientAndDebs(id, page);
                ViewBag.Total = _debService.SumAll(id);
                if (model != null)
                {
                    if (TempData["response"] != null)
                    {
                        ViewBag.response = TempData["response"].ToString();
                        ViewBag.icon = TempData["icon"].ToString();
                    }
                    else
                    {
                        ViewBag.response = null;
                    }
                }
                return View(model);
            }
            else if (_repository.VerifyClientWithAccount(User.Identity.GetUserId(),id))
            {
                var model = _repository.GetWithClientAndDebs(id, page);
                ViewBag.Total = _debService.SumAll(id);
                if (model != null)
                {
                    if (TempData["response"] != null)
                    {
                        ViewBag.response = TempData["response"].ToString();
                        ViewBag.icon = TempData["icon"].ToString();
                    }
                    else
                    {
                        ViewBag.response = null;
                    }
                }
                return View(model);
            }
            return RedirectToAction("MyAccounts");
        }

        [HttpGet]
        public ActionResult Filter(FilterDebsViewModel vm, int page = 1)
        {
            var model = _debService.Filter(vm, page);
            if (model != null)
            {
                ViewBag.pagination = "1";
                ViewBag.paramOne = vm.From;
                ViewBag.paramTwo = vm.To;
                ViewBag.other = vm.IdAccount;
                return View("Detail", model);
            }
            return View("Detail", _repository.GetWithClientAndDebs(vm.IdAccount));
        }
    }
}