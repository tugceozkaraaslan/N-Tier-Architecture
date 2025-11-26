using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKM.Entities
{
    /// <summary>
    /// Araç
    /// Gömülü sistem bileşenlerini barındıran araç varlığı
    /// </summary>
    public class Vehicle
    {
        public int Id { get; set; }
        public string VIN { get; set; }  // Vehicle Identification Number
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public VehicleType Type { get; set; }
        public int Mileage { get; set; }
        public DateTime LastServiceDate { get; set; }
    }

    /// <summary>
    /// Araç Türleri
    /// </summary>
    public enum VehicleType
    {
        Sedan,
        SUV,
        Hatchback,
        Truck,
        Van,
        ElectricVehicle,
        HybridVehicle,
        Motorcycle
    }
}
