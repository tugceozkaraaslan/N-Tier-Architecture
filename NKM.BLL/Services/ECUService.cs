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
    /// ECU Service - İş Mantığı Katmanı
    /// Elektronik Kontrol Ünitesi yönetimi için servis
    /// </summary>
    public class ECUService
    {
        private readonly IECURepository _repository;

        public ECUService(IECURepository repository)
        {
            _repository = repository;
        }

        public void AddECU(ECU ecu)
        {
            if (string.IsNullOrWhiteSpace(ecu.Name))
                throw new Exception("ECU adı boş olamaz.");
            if (string.IsNullOrWhiteSpace(ecu.Manufacturer))
                throw new Exception("Üretici bilgisi boş olamaz.");
            if (string.IsNullOrWhiteSpace(ecu.FirmwareVersion))
                throw new Exception("Firmware versiyonu boş olamaz.");

            _repository.Add(ecu);
        }

        public void UpdateECU(ECU ecu)
        {
            if (ecu.Id <= 0)
                throw new Exception("Geçerli bir ECU ID'si gereklidir.");

            _repository.Update(ecu);
        }

        public void DeleteECU(int id)
        {
            _repository.Delete(id);
        }

        public ECU GetECUById(int id)
        {
            return _repository.GetById(id);
        }

        public List<ECU> GetAllECUs()
        {
            return _repository.GetAll();
        }

        public List<ECU> GetECUsByType(ECUType type)
        {
            return _repository.GetByType(type);
        }

        public List<ECU> GetActiveECUs()
        {
            return _repository.GetActiveUnits();
        }

        public void RunDiagnostic(int ecuId)
        {
            var ecu = _repository.GetById(ecuId);
            if (ecu == null)
                throw new Exception("ECU bulunamadı.");

            ecu.LastDiagnosticDate = DateTime.Now;
            _repository.Update(ecu);
        }
    }
}
