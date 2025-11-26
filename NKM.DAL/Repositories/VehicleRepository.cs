using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKM.DAL.Interfaces;
using NKM.Entities;

namespace NKM.DAL.Repositories
{
    /// <summary>
    /// Vehicle Repository Implementation
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>();
        private static int _nextId = 1;

        public void Add(Vehicle vehicle)
        {
            vehicle.Id = _nextId++;
            _vehicles.Add(vehicle);
        }

        public void Update(Vehicle vehicle)
        {
            var existing = GetById(vehicle.Id);
            if (existing != null)
            {
                existing.VIN = vehicle.VIN;
                existing.Brand = vehicle.Brand;
                existing.Model = vehicle.Model;
                existing.Year = vehicle.Year;
                existing.Type = vehicle.Type;
                existing.Mileage = vehicle.Mileage;
                existing.LastServiceDate = vehicle.LastServiceDate;
            }
        }

        public void Delete(int id)
        {
            var vehicle = GetById(id);
            if (vehicle != null)
            {
                _vehicles.Remove(vehicle);
            }
        }

        public Vehicle GetById(int id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        public Vehicle GetByVIN(string vin)
        {
            return _vehicles.FirstOrDefault(v => v.VIN == vin);
        }

        public List<Vehicle> GetAll()
        {
            return _vehicles.ToList();
        }

        public List<Vehicle> GetByType(VehicleType type)
        {
            return _vehicles.Where(v => v.Type == type).ToList();
        }
    }
}
