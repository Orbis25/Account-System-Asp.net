using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Model.ViewModels;

namespace AccountSystem.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IClientService _clientServece;
        public RequestController(IRequestService requestService , IClientService clientService)
        {
            _requestService = requestService;
            _clientServece = clientService;
        }

        public ActionResult AllRequests(int page = 1)
        {
            return View(_requestService.GetAll(page));
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyRequests(string id)
        {
            UserRequestViewModel model = new UserRequestViewModel
            {
                Client = _clientServece.GetByIdUser(id),
                Requests = _requestService.GetAllByIdUser(id)
            };

            if (TempData["request"] != null)
            {
                ViewBag.status = TempData["request"].ToString();
            }

            return View(model);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Request model)
        {
            if (model.Descripcion != null)
            {
                if(await _requestService.Add(model))
                {
                    TempData["request"] = "Solicitud enviada correctamente";
                }
            }
            return RedirectToAction("MyRequests", new { id = User.Identity.GetUserId() });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                Request request = new Request
                {
                    Id = model.Id,
                    State = model.State
                };
                if (_requestService.Update(request))
                {
                    return RedirectToAction("AllRequests");
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Remove(int id)
        {
            return Json(_requestService.Delete(id));
        }

    }
}