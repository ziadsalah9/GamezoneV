using GamezoneV.Models;
using GamezoneV.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamezoneV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGamesServices _gamesServices;

        public HomeController(ILogger<HomeController> logger, IGamesServices gamesServices)
        {
            _logger = logger;
            
            _gamesServices = gamesServices;
        }

        public IActionResult Index()
        {
           var games= _gamesServices.GetAll();
            return View(games);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
