using System.Collections.Generic;
using System.Linq;
using System.Text;
using Homework3.Models;
using Homework3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Homework3.Controllers
{
    public class CitiesController : Controller
    {

        private readonly ICityService _cityService;

        //It's useful for console output
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ICityService cityService, ILogger<CitiesController> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

       

        public IActionResult List()
        {
            return View(_cityService.GetCities());
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            // Used for dropdown select option
            ViewBag.Cities = _cityService.GetCities()
                .Select(e => new SelectListItem
                {
                    Text = e.CityName,
                    Value = e.Id.ToString()
                }).ToList();

            /*
            ************* Below part will be used to get city and its data (Covids) *************
            */

            // Create a City object
            City city;

            // Get the city object by id
            city = _cityService.GetCity(id);

            // Order by date
            city.Covids = city.Covids.OrderBy(covid => covid.Date).ToList();

            // Create a new list
            List<Covid19> covid19list = new List<Covid19>();

            // Give a reference of city.Covids to the list
            covid19list = city.Covids;

            // This will be shown for table report
            ViewBag.Covid19s = covid19list;

            // Create lists to be used for reporting on the line-chart
            List<string> dates = new List<string>();
            List<long> cases = new List<long>();
            foreach (var a in covid19list)
            {
                dates.Add(a.Date.ToString("d"));  // More reference at https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=netcore-3.1
                cases.Add(a.Cases);
            }

            // Create ViewBags
            ViewBag.Dates = dates;
            ViewBag.Cases = cases;

            return View(_cityService.GetCity(id));
        }

        

        [HttpGet]
        public IActionResult Death(int id)
        {
            // Used for dropdown select option
            ViewBag.Cities = _cityService.GetCities()
                .Select(e => new SelectListItem
                {
                    Text = e.CityName,
                    Value = e.Id.ToString()
                }).ToList();

            /*
            ************* Below part will be used to get city and its data (Covids) *************
            */

            // Create a City object
            City city;

            // Get the city object by id
            city = _cityService.GetCity(id);

            // Create a new list
            List<Covid19> covid19list = new List<Covid19>();

            // Give a reference of city.Covids to the list
            covid19list = city.Covids;

            // This will be shown for table report
            ViewBag.Covid19s = covid19list;

            // Create lists to be used for reporting on the line-chart
            List<string> dates = new List<string>();
            List<long> deaths = new List<long>();
            foreach (var a in covid19list)
            {
                dates.Add(a.Date.ToString("d"));  // More reference at https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=netcore-3.1
                deaths.Add(a.Deaths);
            }

            // Create ViewBags
            ViewBag.Dates = dates;
            ViewBag.Deaths = deaths;

            return View(_cityService.GetCity(id));
        }



        [HttpGet]
        public IActionResult Tested(int id)
        {
            // Used for dropdown select option
            ViewBag.Cities = _cityService.GetCities()
                .Select(e => new SelectListItem
                {
                    Text = e.CityName,
                    Value = e.Id.ToString()
                }).ToList();

            /*
            ************* Below part will be used to get city and its data (Covids) *************
            */

            // Create a City object
            City city;

            // Get the city object by id
            city = _cityService.GetCity(id);

            // Create a new list
            List<Covid19> covid19list = new List<Covid19>();

            // Give a reference of city.Covids to the list
            covid19list = city.Covids;

            // This will be shown for table report
            ViewBag.Covid19s = covid19list;

            // Create lists to be used for reporting on the line-chart
            List<string> dates = new List<string>();
            List<long> tested = new List<long>();
            foreach (var a in covid19list)
            {
                dates.Add(a.Date.ToString("d"));  // More reference at https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=netcore-3.1
                tested.Add(a.Tested);
            }

            // Create ViewBags
            ViewBag.Dates = dates;
            ViewBag.Tested = tested;

            return View(_cityService.GetCity(id));
        }

    }
}
