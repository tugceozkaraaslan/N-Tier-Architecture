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
    /// Diagnostic Code Repository Implementation
    /// </summary>
    public class DiagnosticCodeRepository : IDiagnosticCodeRepository
    {
        private static List<DiagnosticCode> _codes = new List<DiagnosticCode>();
        private static int _nextId = 1;

        public void Add(DiagnosticCode code)
        {
            code.Id = _nextId++;
            _codes.Add(code);
        }

        public void Update(DiagnosticCode code)
        {
            var existing = GetById(code.Id);
            if (existing != null)
            {
                existing.Code = code.Code;
                existing.Description = code.Description;
                existing.Category = code.Category;
                existing.Severity = code.Severity;
                existing.ECUId = code.ECUId;
                existing.VehicleId = code.VehicleId;
                existing.DetectedDate = code.DetectedDate;
                existing.IsResolved = code.IsResolved;
            }
        }

        public void Delete(int id)
        {
            var code = GetById(id);
            if (code != null)
            {
                _codes.Remove(code);
            }
        }

        public DiagnosticCode GetById(int id)
        {
            return _codes.FirstOrDefault(c => c.Id == id);
        }

        public DiagnosticCode GetByCode(string code)
        {
            return _codes.FirstOrDefault(c => c.Code == code);
        }

        public List<DiagnosticCode> GetAll()
        {
            return _codes.ToList();
        }

        public List<DiagnosticCode> GetByVehicleId(int vehicleId)
        {
            return _codes.Where(c => c.VehicleId == vehicleId).ToList();
        }

        public List<DiagnosticCode> GetByECUId(int ecuId)
        {
            return _codes.Where(c => c.ECUId == ecuId).ToList();
        }

        public List<DiagnosticCode> GetUnresolvedCodes()
        {
            return _codes.Where(c => !c.IsResolved).ToList();
        }

        public List<DiagnosticCode> GetBySeverity(DiagnosticSeverity severity)
        {
            return _codes.Where(c => c.Severity == severity).ToList();
        }
    }
}
