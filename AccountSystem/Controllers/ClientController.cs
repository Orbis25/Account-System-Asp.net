using Model;
using Model.ViewModels;
using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
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
            var model = _repository.GetAllIndex(page);
            if (model != null)
            {
               return View(model);
            }
            else
            {
               return RedirectToAction("PageNotFound", "Error");
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Client model)
        {
                if (ModelState.IsValid)
                {
                    if (_repository.Update(model))
                    {
                        return RedirectToAction("UserProfile", "Account", new { id = model.ApplicationUserId });
                    }
                }
                else
                {
                    return RedirectToAction("UserProfile", "Account", new { id = model.ApplicationUserId });
                }

            return RedirectToAction("UserProfile", "Account", new { id = model.ApplicationUserId });
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            return Json(_repository.Delete(id));
        }

        [HttpPost]
        public ActionResult UploadAvatar(int id , HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/"),
                    Path.GetFileName(file.FileName));

                    UploadImgViewModel model = new UploadImgViewModel
                    {
                        Avatar = fileName.ToString(),
                        ClientId = id
                    };
                    _repository.UpdateImg(model);

                    file.SaveAs(path);
                    ViewBag.Message = "Your message for success";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Please select file";
            }
            return RedirectToAction("UserProfile","Account");
        }
    }
}