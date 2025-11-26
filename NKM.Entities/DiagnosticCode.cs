using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKM.Entities
{
    /// <summary>
    /// Arıza Kodu (DTC - Diagnostic Trouble Code)
    /// OBD-II standardına uygun araç arıza kodları
    /// </summary>
    public class DiagnosticCode
    {
        public int Id { get; set; }
        public string Code { get; set; }           // Örn: P0301, P0420
        public string Description { get; set; }
        public DiagnosticCategory Category { get; set; }
        public DiagnosticSeverity Severity { get; set; }
        public int ECUId { get; set; }
        public int VehicleId { get; set; }
        public DateTime DetectedDate { get; set; }
        public bool IsResolved { get; set; }
    }

    /// <summary>
    /// Arıza Kodu Kategorileri (OBD-II standardı)
    /// </summary>
    public enum DiagnosticCategory
    {
        Powertrain,    // P kodları - Güç aktarma organları
        Body,          // B kodları - Gövde
        Chassis,       // C kodları - Şasi
        Network        // U kodları - Ağ/İletişim
    }

    /// <summary>
    /// Arıza Şiddeti
    /// </summary>
    public enum DiagnosticSeverity
    {
        Information,   // Bilgi
        Warning,       // Uyarı
        Critical       // Kritik
    }
}
