using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKM.Entities
{
    /// <summary>
    /// Elektronik Kontrol Ünitesi (Electronic Control Unit)
    /// Otomotiv gömülü sistemlerinde kullanılan temel bileşen
    /// </summary>
    public class ECU
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string FirmwareVersion { get; set; }
        public ECUType Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastDiagnosticDate { get; set; }
    }

    /// <summary>
    /// ECU Türleri
    /// </summary>
    public enum ECUType
    {
        EngineControlModule,      // Motor Kontrol Modülü
        TransmissionControlUnit,  // Şanzıman Kontrol Ünitesi
        BrakeControlModule,       // Fren Kontrol Modülü (ABS/ESP)
        AirbagControlUnit,        // Hava Yastığı Kontrol Ünitesi
        BodyControlModule,        // Gövde Kontrol Modülü
        InstrumentCluster,        // Gösterge Paneli
        InfotainmentSystem,       // Bilgi-Eğlence Sistemi
        BatteryManagementSystem,  // Akü Yönetim Sistemi (EV)
        ADAS                      // Gelişmiş Sürücü Destek Sistemi
    }
}
