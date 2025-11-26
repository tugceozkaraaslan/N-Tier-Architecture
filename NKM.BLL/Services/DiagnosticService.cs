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
    /// Diagnostic Service - İş Mantığı Katmanı
    /// Araç arıza teşhis kodları yönetimi için servis
    /// </summary>
    public class DiagnosticService
    {
        private readonly IDiagnosticCodeRepository _repository;

        public DiagnosticService(IDiagnosticCodeRepository repository)
        {
            _repository = repository;
        }

        public void AddDiagnosticCode(DiagnosticCode code)
        {
            if (string.IsNullOrWhiteSpace(code.Code))
                throw new Exception("Arıza kodu boş olamaz.");
            if (!IsValidDTCFormat(code.Code))
                throw new Exception("Geçersiz DTC formatı. Kod P/B/C/U ile başlamalı ve 4 rakam içermelidir.");
            if (string.IsNullOrWhiteSpace(code.Description))
                throw new Exception("Arıza açıklaması boş olamaz.");

            code.DetectedDate = DateTime.Now;
            _repository.Add(code);
        }

        private bool IsValidDTCFormat(string code)
        {
            if (code.Length != 5) return false;
            
            char prefix = code[0];
            if (prefix != 'P' && prefix != 'B' && prefix != 'C' && prefix != 'U')
                return false;

            for (int i = 1; i < 5; i++)
            {
                if (!char.IsDigit(code[i]))
                    return false;
            }

            return true;
        }

        public void ResolveDiagnosticCode(int codeId)
        {
            var code = _repository.GetById(codeId);
            if (code == null)
                throw new Exception("Arıza kodu bulunamadı.");

            code.IsResolved = true;
            _repository.Update(code);
        }

        public void DeleteDiagnosticCode(int id)
        {
            _repository.Delete(id);
        }

        public DiagnosticCode GetDiagnosticCodeById(int id)
        {
            return _repository.GetById(id);
        }

        public DiagnosticCode GetDiagnosticCodeByCode(string code)
        {
            return _repository.GetByCode(code);
        }

        public List<DiagnosticCode> GetAllDiagnosticCodes()
        {
            return _repository.GetAll();
        }

        public List<DiagnosticCode> GetVehicleDiagnosticCodes(int vehicleId)
        {
            return _repository.GetByVehicleId(vehicleId);
        }

        public List<DiagnosticCode> GetECUDiagnosticCodes(int ecuId)
        {
            return _repository.GetByECUId(ecuId);
        }

        public List<DiagnosticCode> GetUnresolvedCodes()
        {
            return _repository.GetUnresolvedCodes();
        }

        public List<DiagnosticCode> GetCriticalCodes()
        {
            return _repository.GetBySeverity(DiagnosticSeverity.Critical);
        }

        public string GetDTCDescription(string code)
        {
            // OBD-II DTC yorumlama
            if (string.IsNullOrEmpty(code) || code.Length < 1)
                return "Bilinmeyen kod";

            string category;
            switch (code[0])
            {
                case 'P':
                    category = "Güç Aktarma Organları (Powertrain)";
                    break;
                case 'B':
                    category = "Gövde (Body)";
                    break;
                case 'C':
                    category = "Şasi (Chassis)";
                    break;
                case 'U':
                    category = "Ağ/İletişim (Network)";
                    break;
                default:
                    category = "Bilinmeyen Kategori";
                    break;
            }

            return $"Kategori: {category}";
        }
    }
}
