using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKM.DAL.Interfaces;
using NKM.Entities;

namespace NKM.BLL.Services
{
    /// <summary>
    /// Vehicle Service - İş Mantığı Katmanı
    /// Araç yönetimi için servis
    /// </summary>
    public class VehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.VIN))
                throw new Exception("VIN numarası boş olamaz.");
            if (vehicle.VIN.Length != 17)
                throw new Exception("VIN numarası 17 karakter olmalıdır.");
            if (string.IsNullOrWhiteSpace(vehicle.Brand))
                throw new Exception("Marka bilgisi boş olamaz.");
            if (string.IsNullOrWhiteSpace(vehicle.Model))
                throw new Exception("Model bilgisi boş olamaz.");
            if (vehicle.Year < 1900 || vehicle.Year > DateTime.Now.Year + 1)
                throw new Exception("Geçerli bir üretim yılı giriniz.");

            // VIN benzersizlik kontrolü
            var existing = _repository.GetByVIN(vehicle.VIN);
            if (existing != null)
                throw new Exception("Bu VIN numarasına sahip araç zaten kayıtlı.");

            _repository.Add(vehicle);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle.Id <= 0)
                throw new Exception("Geçerli bir araç ID'si gereklidir.");

            _repository.Update(vehicle);
        }

        public void DeleteVehicle(int id)
        {
            _repository.Delete(id);
        }

        public Vehicle GetVehicleById(int id)
        {
            return _repository.GetById(id);
        }

        public Vehicle GetVehicleByVIN(string vin)
        {
            return _repository.GetByVIN(vin);
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _repository.GetAll();
        }

        public List<Vehicle> GetVehiclesByType(VehicleType type)
        {
            return _repository.GetByType(type);
        }

        public void UpdateMileage(int vehicleId, int mileage)
        {
            var vehicle = _repository.GetById(vehicleId);
            if (vehicle == null)
                throw new Exception("Araç bulunamadı.");
            if (mileage < vehicle.Mileage)
                throw new Exception("Yeni kilometre değeri mevcut değerden küçük olamaz.");

            vehicle.Mileage = mileage;
            _repository.Update(vehicle);
        }

        public void RecordService(int vehicleId)
        {
            var vehicle = _repository.GetById(vehicleId);
            if (vehicle == null)
                throw new Exception("Araç bulunamadı.");

            vehicle.LastServiceDate = DateTime.Now;
            _repository.Update(vehicle);
        }
    }
}
