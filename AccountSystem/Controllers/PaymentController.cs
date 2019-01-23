using Model;
using Model.ViewModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AccountSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentController : Controller
    {

        private readonly IPaymentService _paymentService;
        private readonly IDebService _debService;
        public PaymentController(IPaymentService paymentService , IDebService debService)
        {
            _paymentService = paymentService;
            _debService = debService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetAll(int id)
        {
            return Json( new { Payments = await _paymentService.GetAll(id) } ,JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Add(AddPaymentViewModel model)
        {
            Payment payment = new Payment
            {
                CreatedAt = DateTime.Now,
                DebId = model.DebId,
                Id = model.Id,
                Quantity = model.Quantity
            };

            if (ModelState.IsValid && model.Quantity > 0 && _debService.Get(payment.DebId).Deleted != Model.Enums.Deleted.yes)
            {
                if (await _paymentService.Add(payment)) {
                    TempData["response"] = "Pago realizado con exito";
                    TempData["icon"] = "success";
                }
                else
                {
                    TempData["response"] = "Lo sentimos, no puede hacer un abono";
                    TempData["icon"] = "error";
                }

            }
            else
            {
                TempData["response"] = "Lo sentimos, ha ocurrido un error";
                TempData["icon"] = "error";
            }
           return RedirectToAction("Detail", "AccountClient", new { id = model.AccountId });
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await _paymentService.Delete(id));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> GetById(int id)
        {
            var model = await _paymentService.GetById(id);
            if (model != null) return Json(new { payment = model },JsonRequestBehavior.AllowGet);
            return Json(null);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Update(Payment model)
        {            
            if (ModelState.IsValid)
            {
                if (await _paymentService.Update(model))
                {
                    TempData["response"] = "Pago Actualizado";
                    TempData["icon"] = "success";
                }
                else
                {
                    TempData["response"] = "Lo sentimos , revise que no sobre pase el limite del total";
                    TempData["icon"] = "error";
                }

            }
            return RedirectToAction("Detail","AccountClient", new { id = _debService.Get(model.DebId).AccountId});
        }

        [HttpPost]
        public async Task<JsonResult> PayAll(int id)
        {
            return Json(await _paymentService.PayAll(id));
        }
    }
}