using Homework3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework3.Services
{
    public interface ICityService
    {
        List<City> GetCities();
        City GetCity(int id);
        void AddCity(City c);
        void SaveChanges();
    }

    public class CityService : ICityService
    {
        private readonly AppDbContext _db;

        public CityService(AppDbContext db)
        {
            _db = db;
        }

        public List<City> GetCities()
        {
            return _db.Cities.ToList();
        }

        public City GetCity(int id)
        {
            // It returns a city including 
            return _db.Cities.Where(c => c.Id == id).Include(c => c.Covids).SingleOrDefault();
        }

        public void AddCity(City c)
        {
            _db.Cities.Add(c);
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
