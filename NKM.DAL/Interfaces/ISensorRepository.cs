using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKM.Entities;

namespace NKM.DAL.Interfaces
{
    /// <summary>
    /// Sensor Repository Interface
    /// </summary>
    public interface ISensorRepository
    {
        void Add(Sensor sensor);
        void Update(Sensor sensor);
        void Delete(int id);
        Sensor GetById(int id);
        List<Sensor> GetAll();
        List<Sensor> GetByECUId(int ecuId);
        List<Sensor> GetByType(SensorType type);
        List<Sensor> GetFaultySensors();
    }
}
