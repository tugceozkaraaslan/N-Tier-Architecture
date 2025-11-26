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
    /// ECU Repository Implementation
    /// </summary>
    public class ECURepository : IECURepository
    {
        private static List<ECU> _ecus = new List<ECU>();
        private static int _nextId = 1;

        public void Add(ECU ecu)
        {
            ecu.Id = _nextId++;
            _ecus.Add(ecu);
        }

        public void Update(ECU ecu)
        {
            var existing = GetById(ecu.Id);
            if (existing != null)
            {
                existing.Name = ecu.Name;
                existing.Manufacturer = ecu.Manufacturer;
                existing.FirmwareVersion = ecu.FirmwareVersion;
                existing.Type = ecu.Type;
                existing.IsActive = ecu.IsActive;
                existing.LastDiagnosticDate = ecu.LastDiagnosticDate;
            }
        }

        public void Delete(int id)
        {
            var ecu = GetById(id);
            if (ecu != null)
            {
                _ecus.Remove(ecu);
            }
        }

        public ECU GetById(int id)
        {
            return _ecus.FirstOrDefault(e => e.Id == id);
        }

        public List<ECU> GetAll()
        {
            return _ecus.ToList();
        }

        public List<ECU> GetByType(ECUType type)
        {
            return _ecus.Where(e => e.Type == type).ToList();
        }

        public List<ECU> GetActiveUnits()
        {
            return _ecus.Where(e => e.IsActive).ToList();
        }
    }
}
