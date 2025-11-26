using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKM.Entities;

namespace NKM.DAL.Interfaces
{
    /// <summary>
    /// ECU Repository Interface
    /// </summary>
    public interface IECURepository
    {
        void Add(ECU ecu);
        void Update(ECU ecu);
        void Delete(int id);
        ECU GetById(int id);
        List<ECU> GetAll();
        List<ECU> GetByType(ECUType type);
        List<ECU> GetActiveUnits();
    }
}
