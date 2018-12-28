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
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IClientService _clientServece;
        public RequestController(IRequestService requestService , IClientService clientService)
        {
            _requestService = requestService;
            _clientServece = clientService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AllRequests(int page = 1)
        {
            return View(_requestService.GetAllWithPagination(page));
        }

        [HttpGet]
        public ActionResult Search(string parameter, int page = 1)
        {
            var model = _requestService.Search(parameter, page);
            if (model != null)
            {
                ViewBag.pagination = "1";
                ViewBag.parameter = parameter;
                return View("AllRequests",model);
            }
            else
            {
                return RedirectToAction("AllRequests");
            }
        }

        [HttpGet]
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
                ViewBag.icon = TempData["icon"].ToString();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Request model)
        {
            if (model.Descripcion != null)
            {
                if (_clientServece.GetByIdUser(model.ApplicationUserId).ProfileUpdated)
                {
                    if (!_requestService.Exist(model))
                    {
                        if (await _requestService.Add(model))
                        {
                            TempData["request"] = "Solicitud enviada correctamente";
                            TempData["icon"] = "success";
                        }
                        else
                        {
                            TempData["request"] = "Upss!, algo ha ocurrido no se ha enviado la solicitud";
                            TempData["icon"] = "error";
                        }
                    }
                    else
                    {
                        TempData["request"] = "Existe una solicitud con ese nombre,Porfavor tome un nombre diferente";
                        TempData["icon"] = "error";
                    }
                }
                else
                {
                    TempData["request"] = "Lo sentimos!, Para poder hacer solicitudes debe completar su perfil";
                    TempData["icon"] = "error";
                }
            }
            return RedirectToAction("MyRequests", new { id = User.Identity.GetUserId()});
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