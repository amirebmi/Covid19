using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Homework3.Models;
using Homework3.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework3.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly ICityService _cityService;

        public HomeController(ICityService cityService)
        {
            _cityService = cityService;
        }

      //  public HomeController(ILogger<HomeController> logger)
        //{
       //     _logger = logger;
       // }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_cityService.GetCities().ToList());
        }

        public IActionResult Index(City c)
        {
            //return View(_cityService.GetCity(c.Id));
            return RedirectToAction("View", "Cities", new { id = c.Id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
