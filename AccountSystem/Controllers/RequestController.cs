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
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: Request
        public ActionResult AllRequests()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Request model)
        {
            await _requestService.Add(model);
            return RedirectToAction("AllRequest");
        }
    }
}