using AnbimaFeedMVC.Models;
using AnbimaFeedMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnbimaFeedMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDebenturesMercadoSecundarioApiServices _debenturesMercadoSecundarioApiServices;

        public HomeController(IDebenturesMercadoSecundarioApiServices debenturesMercadoSecundarioApiServices)
        {
            _debenturesMercadoSecundarioApiServices = debenturesMercadoSecundarioApiServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateTime data)
        {
            return RedirectToAction("GetDebenturesMercadoSecundario", new { data = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetDebenturesMercadoSecundario(DateTime data)
        {
            List<DebenturesMercadoSecundarioModel> debenturesMercadoSecundario = await _debenturesMercadoSecundarioApiServices.GetDebenturesMercadoSecundario(data);

            return View("Index", debenturesMercadoSecundario);
        }
    }
}