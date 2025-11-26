using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKM.Entities;

namespace NKM.DAL.Interfaces
{
    /// <summary>
    /// Vehicle Repository Interface
    /// </summary>
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(int id);
        Vehicle GetById(int id);
        Vehicle GetByVIN(string vin);
        List<Vehicle> GetAll();
        List<Vehicle> GetByType(VehicleType type);
    }
}
