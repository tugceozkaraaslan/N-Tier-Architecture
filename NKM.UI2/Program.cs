using NKM.BLL;
using NKM.BLL.Services;
using NKM.DAL;
using NKM.DAL.NKM.DAL;
using NKM.DAL.Interfaces;
using NKM.DAL.Repositories;
using NKM.Entities;
using System;


namespace NKM.ConsoleUI
{
    public class Program
    {
        // Services
        static IProductRepository productRepository = new ProductRepository();
        static ProductService productService;
        static IECURepository ecuRepository = new ECURepository();
        static ECUService ecuService;
        static ISensorRepository sensorRepository = new SensorRepository();
        static SensorService sensorService;
        static IVehicleRepository vehicleRepository = new VehicleRepository();
        static VehicleService vehicleService;
        static IDiagnosticCodeRepository diagnosticRepository = new DiagnosticCodeRepository();
        static DiagnosticService diagnosticService;
        static ILogger logger = new Logger();

        static void Main(string[] args)
        { 
            // Initialize services
            productService = new ProductService(productRepository);
            ecuService = new ECUService(ecuRepository);
            sensorService = new SensorService(sensorRepository);
            vehicleService = new VehicleService(vehicleRepository);
            diagnosticService = new DiagnosticService(diagnosticRepository);

            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     OTOMOTİV GÖMÜLÜ SİSTEM YÖNETİM UYGULAMASI              ║");
            Console.WriteLine("║     Automotive Embedded System Management Application      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            while (true)
            {
                ShowMainMenu();
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        VehicleMenu();
                        break;
                    case "2":
                        ECUMenu();
                        break;
                    case "3":
                        SensorMenu();
                        break;
                    case "4":
                        DiagnosticMenu();
                        break;
                    case "5":
                        ProductMenu();
                        break;
                    case "0":
                        Console.WriteLine("Güle güle! / Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim! / Invalid selection!");
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine("            ANA MENÜ / MAIN MENU");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("1- Araç Yönetimi (Vehicle Management)");
            Console.WriteLine("2- ECU Yönetimi (ECU Management)");
            Console.WriteLine("3- Sensör Yönetimi (Sensor Management)");
            Console.WriteLine("4- Arıza Kodları (Diagnostic Codes)");
            Console.WriteLine("5- Ürün Yönetimi (Product Management)");
            Console.WriteLine("0- Çıkış (Exit)");
            Console.Write("Seçiminiz / Your choice: ");
        }

        #region Vehicle Management
        static void VehicleMenu()
        {
            while (true)
            {
                Console.WriteLine("\n─────────────────────────────────────");
                Console.WriteLine("      ARAÇ YÖNETİMİ / VEHICLE MGMT");
                Console.WriteLine("─────────────────────────────────────");
                Console.WriteLine("1- Araç Ekle (Add Vehicle)");
                Console.WriteLine("2- Araçları Listele (List Vehicles)");
                Console.WriteLine("3- Kilometre Güncelle (Update Mileage)");
                Console.WriteLine("4- Servis Kaydı (Record Service)");
                Console.WriteLine("0- Geri (Back)");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "0") break;

                try
                {
                    switch (secim)
                    {
                        case "1":
                            AddVehicle();
                            break;
                        case "2":
                            ListVehicles();
                            break;
                        case "3":
                            UpdateVehicleMileage();
                            break;
                        case "4":
                            RecordVehicleService();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata / Error: {ex.Message}");
                    logger.Log(ex);
                }
            }
        }

        static void AddVehicle()
        {
            Console.Write("VIN (17 karakter): ");
            string vin = Console.ReadLine();
            Console.Write("Marka / Brand: ");
            string brand = Console.ReadLine();
            Console.Write("Model: ");
            string model = Console.ReadLine();
            Console.Write("Yıl / Year: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Araç Tipi / Vehicle Type:");
            Console.WriteLine("0-Sedan, 1-SUV, 2-Hatchback, 3-Truck, 4-Van, 5-ElectricVehicle, 6-HybridVehicle, 7-Motorcycle");
            Console.Write("Tip / Type: ");
            VehicleType type = (VehicleType)Convert.ToInt32(Console.ReadLine());
            Console.Write("Kilometre / Mileage: ");
            int mileage = Convert.ToInt32(Console.ReadLine());

            Vehicle vehicle = new Vehicle
            {
                VIN = vin,
                Brand = brand,
                Model = model,
                Year = year,
                Type = type,
                Mileage = mileage,
                LastServiceDate = DateTime.Now
            };

            vehicleService.AddVehicle(vehicle);
            Console.WriteLine("✓ Araç eklendi! / Vehicle added!");
        }

        static void ListVehicles()
        {
            var vehicles = vehicleService.GetAllVehicles();
            if (vehicles.Count == 0)
            {
                Console.WriteLine("Kayıtlı araç yok. / No vehicles registered.");
                return;
            }

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                           KAYITLI ARAÇLAR                                 ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════╣");
            foreach (var v in vehicles)
            {
                Console.WriteLine($"║ ID: {v.Id} | VIN: {v.VIN}");
                Console.WriteLine($"║ {v.Brand} {v.Model} ({v.Year}) - {v.Type}");
                Console.WriteLine($"║ Km: {v.Mileage:N0} | Son Servis: {v.LastServiceDate:dd/MM/yyyy}");
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════╣");
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════╝");
        }

        static void UpdateVehicleMileage()
        {
            Console.Write("Araç ID / Vehicle ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Yeni Kilometre / New Mileage: ");
            int mileage = Convert.ToInt32(Console.ReadLine());
            vehicleService.UpdateMileage(id, mileage);
            Console.WriteLine("✓ Kilometre güncellendi! / Mileage updated!");
        }

        static void RecordVehicleService()
        {
            Console.Write("Araç ID / Vehicle ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            vehicleService.RecordService(id);
            Console.WriteLine("✓ Servis kaydı eklendi! / Service recorded!");
        }
        #endregion

        #region ECU Management
        static void ECUMenu()
        {
            while (true)
            {
                Console.WriteLine("\n─────────────────────────────────────");
                Console.WriteLine("      ECU YÖNETİMİ / ECU MGMT");
                Console.WriteLine("─────────────────────────────────────");
                Console.WriteLine("1- ECU Ekle (Add ECU)");
                Console.WriteLine("2- ECU'ları Listele (List ECUs)");
                Console.WriteLine("3- Aktif ECU'lar (Active ECUs)");
                Console.WriteLine("4- Tanılama Çalıştır (Run Diagnostic)");
                Console.WriteLine("0- Geri (Back)");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "0") break;

                try
                {
                    switch (secim)
                    {
                        case "1":
                            AddECU();
                            break;
                        case "2":
                            ListECUs();
                            break;
                        case "3":
                            ListActiveECUs();
                            break;
                        case "4":
                            RunECUDiagnostic();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata / Error: {ex.Message}");
                    logger.Log(ex);
                }
            }
        }

        static void AddECU()
        {
            Console.Write("ECU Adı / Name: ");
            string name = Console.ReadLine();
            Console.Write("Üretici / Manufacturer: ");
            string manufacturer = Console.ReadLine();
            Console.Write("Firmware Versiyonu / Firmware Version: ");
            string firmware = Console.ReadLine();
            Console.WriteLine("ECU Tipi / Type:");
            Console.WriteLine("0-EngineControlModule, 1-TransmissionControlUnit, 2-BrakeControlModule");
            Console.WriteLine("3-AirbagControlUnit, 4-BodyControlModule, 5-InstrumentCluster");
            Console.WriteLine("6-InfotainmentSystem, 7-BatteryManagementSystem, 8-ADAS");
            Console.Write("Tip / Type: ");
            ECUType type = (ECUType)Convert.ToInt32(Console.ReadLine());

            ECU ecu = new ECU
            {
                Name = name,
                Manufacturer = manufacturer,
                FirmwareVersion = firmware,
                Type = type,
                IsActive = true,
                LastDiagnosticDate = DateTime.Now
            };

            ecuService.AddECU(ecu);
            Console.WriteLine("✓ ECU eklendi! / ECU added!");
        }

        static void ListECUs()
        {
            var ecus = ecuService.GetAllECUs();
            if (ecus.Count == 0)
            {
                Console.WriteLine("Kayıtlı ECU yok. / No ECUs registered.");
                return;
            }

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ELEKTRONİK KONTROL ÜNİTELERİ (ECU)               ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
            foreach (var e in ecus)
            {
                string status = e.IsActive ? "✓ Aktif" : "✗ Pasif";
                Console.WriteLine($"║ ID: {e.Id} | {e.Name} ({e.Type})");
                Console.WriteLine($"║ Üretici: {e.Manufacturer} | FW: {e.FirmwareVersion}");
                Console.WriteLine($"║ Durum: {status} | Son Tanılama: {e.LastDiagnosticDate:dd/MM/yyyy}");
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        }

        static void ListActiveECUs()
        {
            var ecus = ecuService.GetActiveECUs();
            Console.WriteLine($"\nAktif ECU sayısı: {ecus.Count}");
            foreach (var e in ecus)
            {
                Console.WriteLine($"  • {e.Name} - {e.Type}");
            }
        }

        static void RunECUDiagnostic()
        {
            Console.Write("ECU ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            ecuService.RunDiagnostic(id);
            Console.WriteLine("✓ Tanılama tamamlandı! / Diagnostic completed!");
        }
        #endregion

        #region Sensor Management
        static void SensorMenu()
        {
            while (true)
            {
                Console.WriteLine("\n─────────────────────────────────────");
                Console.WriteLine("    SENSÖR YÖNETİMİ / SENSOR MGMT");
                Console.WriteLine("─────────────────────────────────────");
                Console.WriteLine("1- Sensör Ekle (Add Sensor)");
                Console.WriteLine("2- Sensörleri Listele (List Sensors)");
                Console.WriteLine("3- Arızalı Sensörler (Faulty Sensors)");
                Console.WriteLine("4- Sensör Okuması Güncelle (Update Reading)");
                Console.WriteLine("5- Sensör Kalibre Et (Calibrate Sensor)");
                Console.WriteLine("0- Geri (Back)");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "0") break;

                try
                {
                    switch (secim)
                    {
                        case "1":
                            AddSensor();
                            break;
                        case "2":
                            ListSensors();
                            break;
                        case "3":
                            ListFaultySensors();
                            break;
                        case "4":
                            UpdateSensorReading();
                            break;
                        case "5":
                            CalibrateSensor();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata / Error: {ex.Message}");
                    logger.Log(ex);
                }
            }
        }

        static void AddSensor()
        {
            Console.Write("Sensör Adı / Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Sensör Tipi / Type:");
            Console.WriteLine("0-Temperature, 1-Pressure, 2-Speed, 3-Acceleration, 4-Proximity");
            Console.WriteLine("5-Oxygen, 6-Knock, 7-Crankshaft, 8-Camshaft, 9-ThrottlePosition");
            Console.WriteLine("10-MassAirFlow, 11-Radar, 12-Lidar, 13-Camera, 14-Ultrasonic");
            Console.Write("Tip / Type: ");
            SensorType type = (SensorType)Convert.ToInt32(Console.ReadLine());
            Console.Write("Minimum Değer / Min Value: ");
            double minValue = Convert.ToDouble(Console.ReadLine());
            Console.Write("Maksimum Değer / Max Value: ");
            double maxValue = Convert.ToDouble(Console.ReadLine());
            Console.Write("Birim / Unit (örn: °C, bar, km/h): ");
            string unit = Console.ReadLine();
            Console.Write("Bağlı ECU ID / Connected ECU ID: ");
            int ecuId = Convert.ToInt32(Console.ReadLine());

            Sensor sensor = new Sensor
            {
                Name = name,
                Type = type,
                MinValue = minValue,
                MaxValue = maxValue,
                Unit = unit,
                ECUId = ecuId,
                Status = SensorStatus.Active,
                CurrentValue = 0
            };

            sensorService.AddSensor(sensor);
            Console.WriteLine("✓ Sensör eklendi! / Sensor added!");
        }

        static void ListSensors()
        {
            var sensors = sensorService.GetAllSensors();
            if (sensors.Count == 0)
            {
                Console.WriteLine("Kayıtlı sensör yok. / No sensors registered.");
                return;
            }

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      SENSÖR LİSTESİ                           ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
            foreach (var s in sensors)
            {
                string statusSymbol = s.Status == SensorStatus.Active ? "✓" : 
                                     s.Status == SensorStatus.Faulty ? "✗" : "○";
                Console.WriteLine($"║ ID: {s.Id} | {s.Name} ({s.Type})");
                Console.WriteLine($"║ Aralık: {s.MinValue}-{s.MaxValue} {s.Unit}");
                Console.WriteLine($"║ Mevcut Değer: {s.CurrentValue} {s.Unit} | Durum: {statusSymbol} {s.Status}");
                Console.WriteLine($"║ ECU ID: {s.ECUId}");
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        }

        static void ListFaultySensors()
        {
            var sensors = sensorService.GetFaultySensors();
            if (sensors.Count == 0)
            {
                Console.WriteLine("✓ Arızalı sensör yok! / No faulty sensors!");
                return;
            }

            Console.WriteLine($"\n⚠ ARIZALI SENSÖRLER ({sensors.Count} adet):");
            foreach (var s in sensors)
            {
                Console.WriteLine($"  ✗ {s.Name} - Mevcut: {s.CurrentValue} {s.Unit} (Aralık: {s.MinValue}-{s.MaxValue})");
            }
        }

        static void UpdateSensorReading()
        {
            Console.Write("Sensör ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Yeni Değer / New Value: ");
            double value = Convert.ToDouble(Console.ReadLine());
            sensorService.UpdateSensorReading(id, value);
            Console.WriteLine("✓ Sensör okuması güncellendi! / Sensor reading updated!");
        }

        static void CalibrateSensor()
        {
            Console.Write("Sensör ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            sensorService.CalibrateSensor(id);
            Console.WriteLine("✓ Sensör kalibrasyon moduna alındı! / Sensor is calibrating!");
        }
        #endregion

        #region Diagnostic Management
        static void DiagnosticMenu()
        {
            while (true)
            {
                Console.WriteLine("\n─────────────────────────────────────");
                Console.WriteLine("   ARIZA KODLARI / DIAGNOSTIC CODES");
                Console.WriteLine("─────────────────────────────────────");
                Console.WriteLine("1- Arıza Kodu Ekle (Add DTC)");
                Console.WriteLine("2- Tüm Kodları Listele (List All)");
                Console.WriteLine("3- Çözülmemiş Kodlar (Unresolved)");
                Console.WriteLine("4- Kritik Kodlar (Critical)");
                Console.WriteLine("5- Arıza Çöz (Resolve DTC)");
                Console.WriteLine("0- Geri (Back)");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "0") break;

                try
                {
                    switch (secim)
                    {
                        case "1":
                            AddDiagnosticCode();
                            break;
                        case "2":
                            ListAllDiagnosticCodes();
                            break;
                        case "3":
                            ListUnresolvedCodes();
                            break;
                        case "4":
                            ListCriticalCodes();
                            break;
                        case "5":
                            ResolveDiagnosticCode();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata / Error: {ex.Message}");
                    logger.Log(ex);
                }
            }
        }

        static void AddDiagnosticCode()
        {
            Console.Write("Arıza Kodu / DTC (örn: P0301): ");
            string code = Console.ReadLine().ToUpper();
            Console.Write("Açıklama / Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Kategori / Category:");
            Console.WriteLine("0-Powertrain, 1-Body, 2-Chassis, 3-Network");
            Console.Write("Kategori / Category: ");
            DiagnosticCategory category = (DiagnosticCategory)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Şiddet / Severity:");
            Console.WriteLine("0-Information, 1-Warning, 2-Critical");
            Console.Write("Şiddet / Severity: ");
            DiagnosticSeverity severity = (DiagnosticSeverity)Convert.ToInt32(Console.ReadLine());
            Console.Write("Araç ID / Vehicle ID: ");
            int vehicleId = Convert.ToInt32(Console.ReadLine());
            Console.Write("ECU ID: ");
            int ecuId = Convert.ToInt32(Console.ReadLine());

            DiagnosticCode dtc = new DiagnosticCode
            {
                Code = code,
                Description = description,
                Category = category,
                Severity = severity,
                VehicleId = vehicleId,
                ECUId = ecuId,
                IsResolved = false
            };

            diagnosticService.AddDiagnosticCode(dtc);
            Console.WriteLine("✓ Arıza kodu eklendi! / DTC added!");
            Console.WriteLine(diagnosticService.GetDTCDescription(code));
        }

        static void ListAllDiagnosticCodes()
        {
            var codes = diagnosticService.GetAllDiagnosticCodes();
            if (codes.Count == 0)
            {
                Console.WriteLine("Kayıtlı arıza kodu yok. / No DTCs registered.");
                return;
            }

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ARIZA KODLARI (DTC)                        ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
            foreach (var c in codes)
            {
                string status = c.IsResolved ? "✓ Çözüldü" : "✗ Aktif";
                string severitySymbol = c.Severity == DiagnosticSeverity.Critical ? "🔴" :
                                       c.Severity == DiagnosticSeverity.Warning ? "🟡" : "🟢";
                Console.WriteLine($"║ {severitySymbol} {c.Code} - {c.Description}");
                Console.WriteLine($"║ Kategori: {c.Category} | Şiddet: {c.Severity}");
                Console.WriteLine($"║ Araç ID: {c.VehicleId} | ECU ID: {c.ECUId}");
                Console.WriteLine($"║ Tespit: {c.DetectedDate:dd/MM/yyyy HH:mm} | Durum: {status}");
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        }

        static void ListUnresolvedCodes()
        {
            var codes = diagnosticService.GetUnresolvedCodes();
            if (codes.Count == 0)
            {
                Console.WriteLine("✓ Çözülmemiş arıza kodu yok! / No unresolved DTCs!");
                return;
            }

            Console.WriteLine($"\n⚠ ÇÖZÜLMEMIŞ ARIZA KODLARI ({codes.Count} adet):");
            foreach (var c in codes)
            {
                Console.WriteLine($"  ✗ {c.Code}: {c.Description} ({c.Severity})");
            }
        }

        static void ListCriticalCodes()
        {
            var codes = diagnosticService.GetCriticalCodes();
            if (codes.Count == 0)
            {
                Console.WriteLine("✓ Kritik arıza kodu yok! / No critical DTCs!");
                return;
            }

            Console.WriteLine($"\n🔴 KRİTİK ARIZA KODLARI ({codes.Count} adet):");
            foreach (var c in codes)
            {
                Console.WriteLine($"  ⚠ {c.Code}: {c.Description}");
            }
        }

        static void ResolveDiagnosticCode()
        {
            Console.Write("Arıza Kodu ID / DTC ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            diagnosticService.ResolveDiagnosticCode(id);
            Console.WriteLine("✓ Arıza kodu çözüldü olarak işaretlendi! / DTC marked as resolved!");
        }
        #endregion

        #region Product Management (Original)
        static void ProductMenu()
        {
            while (true)
            {
                Console.WriteLine("\n─────────────────────────────────────");
                Console.WriteLine("    ÜRÜN YÖNETİMİ / PRODUCT MGMT");
                Console.WriteLine("─────────────────────────────────────");
                Console.WriteLine("1- Ürün Ekle (Add Product)");
                Console.WriteLine("2- Ürünleri Listele (List Products)");
                Console.WriteLine("0- Geri (Back)");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "0") break;

                try
                {
                    if (secim == "1")
                    {
                        Console.Write("Ürün adı: ");
                        string name = Console.ReadLine();
                        Console.Write("Fiyat: ");
                        double price = Convert.ToDouble(Console.ReadLine());

                        Product p = new Product { Name = name, Price = price };
                        productService.AddProduct(p);
                        Console.WriteLine("✓ Ürün eklendi! / Product added!");
                    }
                    else if (secim == "2")
                    {
                        var products = productService.GetAllProducts();
                        if (products.Count == 0)
                        {
                            Console.WriteLine("Kayıtlı ürün yok. / No products registered.");
                            continue;
                        }
                        foreach (var p in products)
                        {
                            Console.WriteLine($"  • Ad: {p.Name}, Fiyat: {p.Price:C}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata / Error: {ex.Message}");
                    logger.Log(ex);
                }
            }
        }
        #endregion
    }
}
