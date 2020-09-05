using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Homework3.Models;
using Homework3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework3.Controllers
{
    public class FilesController : Controller
    {

        private readonly ICityService _cityService;

        public FilesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Import(IFormFile file, DateTime dateInput)
        {

            // Variables for City 
            string rawCity; // rawCity means city with "". We will use Substring to remove it
            string city;    // this variable does not have ""
            long population;

            // Variables for Covid19
            long cases;
            long deaths;
            long tested;

            // Read all the lines from file
            using var reader = new StreamReader(file.OpenReadStream());
            List<string> lines = new List<string>();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            foreach (var x in lines.Skip(1))
            {
                string[] column = x.Split(",");

                rawCity = column[1];

                city = rawCity.Substring(1, rawCity.Length - 2); // quotation-removed city name
                population = long.Parse(column[11]);

                cases = long.Parse(column[2]);
                deaths = long.Parse(column[5]);
                tested = long.Parse(column[8]);


                //City cityOBJ;
                List<Covid19> covid19List;

                var cityObject = (from c in _cityService.GetCities()
                                     where c.CityName == city
                                     select c).FirstOrDefault();
                // If the city EXISTS in the database, the following code will add Covid19's properties to it (by referencing in the db)
                if (cityObject != null)
                {
                    // Initialize the list
                    covid19List = new List<Covid19>();

                    // Create a new object of Covid19
                    var newCovidObject = new Covid19
                    {
                        Date = dateInput,
                        Cases = cases,
                        Deaths = deaths,
                        Tested = tested
                    };

                    // Add covidObject to the covid19List
                    covid19List.Add(newCovidObject);

                    cityObject.Covids = covid19List; // Update the object

                    _cityService.SaveChanges();
                }   // End if statement
                else
                {
                    // Create a new City object
                    var newCity = new City
                    {
                        CityName = city,
                        Population = population
                    };

                    // Initialize covid19List
                    covid19List = new List<Covid19>();

                    // Create a new 
                    var covidObject = new Covid19
                    {
                        Date = dateInput,
                        Cases = cases,
                        Deaths = deaths,
                        Tested = tested
                    };

                    // Add covidObject to the covid19List
                    covid19List.Add(covidObject);

                    newCity.Covids = covid19List;

                    // After cityObject has a reference of covid19List, it needs to be added to the database
                    _cityService.AddCity(newCity);

                    _cityService.SaveChanges();
                }

            }
            return RedirectToAction("Index","Home");
        }
    }
}
