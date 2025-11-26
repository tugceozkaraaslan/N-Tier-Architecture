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
    /// Sensor Service - İş Mantığı Katmanı
    /// Sensör yönetimi için servis
    /// </summary>
    public class SensorService
    {
        private readonly ISensorRepository _repository;

        public SensorService(ISensorRepository repository)
        {
            _repository = repository;
        }

        public void AddSensor(Sensor sensor)
        {
            if (string.IsNullOrWhiteSpace(sensor.Name))
                throw new Exception("Sensör adı boş olamaz.");
            if (sensor.MinValue >= sensor.MaxValue)
                throw new Exception("Minimum değer maksimum değerden küçük olmalıdır.");
            if (string.IsNullOrWhiteSpace(sensor.Unit))
                throw new Exception("Birim bilgisi boş olamaz.");

            _repository.Add(sensor);
        }

        public void UpdateSensor(Sensor sensor)
        {
            if (sensor.Id <= 0)
                throw new Exception("Geçerli bir sensör ID'si gereklidir.");

            _repository.Update(sensor);
        }

        public void DeleteSensor(int id)
        {
            _repository.Delete(id);
        }

        public Sensor GetSensorById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Sensor> GetAllSensors()
        {
            return _repository.GetAll();
        }

        public List<Sensor> GetSensorsByECU(int ecuId)
        {
            return _repository.GetByECUId(ecuId);
        }

        public List<Sensor> GetSensorsByType(SensorType type)
        {
            return _repository.GetByType(type);
        }

        public List<Sensor> GetFaultySensors()
        {
            return _repository.GetFaultySensors();
        }

        public void UpdateSensorReading(int sensorId, double value)
        {
            var sensor = _repository.GetById(sensorId);
            if (sensor == null)
                throw new Exception("Sensör bulunamadı.");

            if (value < sensor.MinValue || value > sensor.MaxValue)
            {
                sensor.Status = SensorStatus.Faulty;
            }
            else
            {
                sensor.Status = SensorStatus.Active;
            }

            sensor.CurrentValue = value;
            _repository.Update(sensor);
        }

        public void CalibrateSensor(int sensorId)
        {
            var sensor = _repository.GetById(sensorId);
            if (sensor == null)
                throw new Exception("Sensör bulunamadı.");

            sensor.Status = SensorStatus.Calibrating;
            _repository.Update(sensor);
        }
    }
}
