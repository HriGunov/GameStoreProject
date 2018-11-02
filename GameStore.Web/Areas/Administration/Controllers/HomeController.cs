using System.Diagnostics;
using GameStore.Services.Abstract;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]

    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}