using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKM.Entities
{
    /// <summary>
    /// Otomotiv Sensörü
    /// Araç gömülü sistemlerinde veri toplayan sensör bileşeni
    /// </summary>
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SensorType Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string Unit { get; set; }
        public double CurrentValue { get; set; }
        public SensorStatus Status { get; set; }
        public int ECUId { get; set; }
    }

    /// <summary>
    /// Sensör Türleri
    /// </summary>
    public enum SensorType
    {
        Temperature,           // Sıcaklık Sensörü
        Pressure,              // Basınç Sensörü
        Speed,                 // Hız Sensörü
        Acceleration,          // İvme Sensörü
        Proximity,             // Yakınlık Sensörü (Park sensörü)
        Oxygen,                // Oksijen Sensörü (Lambda)
        Knock,                 // Vuruntu Sensörü
        Crankshaft,            // Krank Mili Sensörü
        Camshaft,              // Eksantrik Mili Sensörü
        ThrottlePosition,      // Gaz Kelebeği Pozisyon Sensörü
        MassAirFlow,           // Kütle Hava Akış Sensörü
        Radar,                 // Radar Sensörü (ADAS)
        Lidar,                 // Lidar Sensörü (ADAS)
        Camera,                // Kamera Sensörü (ADAS)
        Ultrasonic             // Ultrasonik Sensör
    }

    /// <summary>
    /// Sensör Durumu
    /// </summary>
    public enum SensorStatus
    {
        Active,      // Aktif
        Inactive,    // Pasif
        Faulty,      // Arızalı
        Calibrating, // Kalibrasyon
        OutOfRange   // Aralık Dışı - geçici durum
    }
}
