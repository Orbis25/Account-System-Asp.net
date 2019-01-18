using Model;
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
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Add(Payment model)
        {
            model.CreatedAt = DateTime.Now;
            if (ModelState.IsValid && model.Quantity > 0)
            {
                await _paymentService.Add(model);
                TempData["response"] = "Pago realizado con exito";
                TempData["icon"] = "success";
            }
            else
            {
                TempData["response"] = "Lo, sentimos ha ocurrido un error";
                TempData["icon"] = "error";
            }
            return RedirectToAction("Detail", "AccountClient", new { id = model.AccountId });
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            return Json(await _paymentService.Delete(id));
        }

        [HttpGet]
        public async Task<JsonResult> Update(int id)
        {
            return Json(new { payment = _paymentService.GetById(id) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Update(Payment model)
        {
            model.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (await _paymentService.Update(model))
                {
                    TempData["response"] = "Pago Actualizado";
                    TempData["icon"] = "success";
                }
                else
                {
                    TempData["response"] = "Lo sentimos , ha ocurrido un error";
                    TempData["icon"] = "error";
                }
                
            }
                TempData["response"] = "Lo sentimos , ha ocurrido un error";
                TempData["icon"] = "error";
            return RedirectToAction("Detail","AccountClient", new { id = model.AccountId});
        }


    }
}