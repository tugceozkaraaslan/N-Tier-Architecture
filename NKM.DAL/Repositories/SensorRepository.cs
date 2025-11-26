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
    /// Sensor Repository Implementation
    /// </summary>
    public class SensorRepository : ISensorRepository
    {
        private static List<Sensor> _sensors = new List<Sensor>();
        private static int _nextId = 1;

        public void Add(Sensor sensor)
        {
            sensor.Id = _nextId++;
            _sensors.Add(sensor);
        }

        public void Update(Sensor sensor)
        {
            var existing = GetById(sensor.Id);
            if (existing != null)
            {
                existing.Name = sensor.Name;
                existing.Type = sensor.Type;
                existing.MinValue = sensor.MinValue;
                existing.MaxValue = sensor.MaxValue;
                existing.Unit = sensor.Unit;
                existing.CurrentValue = sensor.CurrentValue;
                existing.Status = sensor.Status;
                existing.ECUId = sensor.ECUId;
            }
        }

        public void Delete(int id)
        {
            var sensor = GetById(id);
            if (sensor != null)
            {
                _sensors.Remove(sensor);
            }
        }

        public Sensor GetById(int id)
        {
            return _sensors.FirstOrDefault(s => s.Id == id);
        }

        public List<Sensor> GetAll()
        {
            return _sensors.ToList();
        }

        public List<Sensor> GetByECUId(int ecuId)
        {
            return _sensors.Where(s => s.ECUId == ecuId).ToList();
        }

        public List<Sensor> GetByType(SensorType type)
        {
            return _sensors.Where(s => s.Type == type).ToList();
        }

        public List<Sensor> GetFaultySensors()
        {
            return _sensors.Where(s => s.Status == SensorStatus.Faulty).ToList();
        }
    }
}
