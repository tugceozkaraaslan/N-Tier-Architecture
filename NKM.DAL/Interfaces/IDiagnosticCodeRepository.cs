using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKM.Entities;

namespace NKM.DAL.Interfaces
{
    /// <summary>
    /// Diagnostic Code Repository Interface
    /// </summary>
    public interface IDiagnosticCodeRepository
    {
        void Add(DiagnosticCode code);
        void Update(DiagnosticCode code);
        void Delete(int id);
        DiagnosticCode GetById(int id);
        DiagnosticCode GetByCode(string code);
        List<DiagnosticCode> GetAll();
        List<DiagnosticCode> GetByVehicleId(int vehicleId);
        List<DiagnosticCode> GetByECUId(int ecuId);
        List<DiagnosticCode> GetUnresolvedCodes();
        List<DiagnosticCode> GetBySeverity(DiagnosticSeverity severity);
    }
}
