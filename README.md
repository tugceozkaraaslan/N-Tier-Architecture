# Otomotiv Gömülü Sistem Yönetim Projesi
# Automotive Embedded System Management Project

Bu proje, N-Tier (Çok Katmanlı) mimari kullanılarak geliştirilmiş bir **otomotiv gömülü sistemler** yönetim uygulamasıdır.

This project is an **automotive embedded systems** management application developed using N-Tier Architecture.

## 🚗 Proje Özellikleri / Features

### Araç Yönetimi (Vehicle Management)
- Araç kaydı (VIN, marka, model, yıl, tip)
- Kilometre takibi
- Servis kayıtları
- Araç tipi filtreleme (Sedan, SUV, Elektrikli, Hibrit vb.)

### ECU Yönetimi (Electronic Control Unit Management)
- ECU kaydı ve takibi
- Üretici ve firmware versiyonu bilgisi
- ECU türleri:
  - Motor Kontrol Modülü (Engine Control Module)
  - Şanzıman Kontrol Ünitesi (Transmission Control Unit)
  - Fren Kontrol Modülü - ABS/ESP (Brake Control Module)
  - Hava Yastığı Kontrol Ünitesi (Airbag Control Unit)
  - Gövde Kontrol Modülü (Body Control Module)
  - Gösterge Paneli (Instrument Cluster)
  - Bilgi-Eğlence Sistemi (Infotainment System)
  - Akü Yönetim Sistemi - EV (Battery Management System)
  - ADAS - Gelişmiş Sürücü Destek Sistemi (Advanced Driver Assistance System)
- Tanılama çalıştırma

### Sensör Yönetimi (Sensor Management)
- Sensör kaydı ve izleme
- Sensör türleri:
  - Sıcaklık, Basınç, Hız, İvme sensörleri
  - Oksijen (Lambda), Vuruntu sensörleri
  - Krank/Eksantrik mili sensörleri
  - Gaz kelebeği pozisyon sensörü
  - Kütle hava akış sensörü
  - ADAS sensörleri (Radar, Lidar, Kamera, Ultrasonik)
- Sensör okuma değerlerini güncelleme
- Arızalı sensör tespiti
- Sensör kalibrasyon modu

### Arıza Kodları (Diagnostic Trouble Codes - DTC)
- OBD-II standardına uygun arıza kodları
- DTC kategorileri:
  - P kodları: Güç Aktarma Organları (Powertrain)
  - B kodları: Gövde (Body)
  - C kodları: Şasi (Chassis)
  - U kodları: Ağ/İletişim (Network)
- Şiddet seviyeleri (Bilgi, Uyarı, Kritik)
- Çözümlenmemiş arıza takibi

## 🏗️ Mimari / Architecture

```
┌─────────────────────────────────────────────────┐
│                  UI Layer                        │
│              (NKM.UI2 - Console)                 │
├─────────────────────────────────────────────────┤
│           Business Logic Layer                   │
│                 (NKM.BLL)                        │
│  ┌─────────────────────────────────────────────┐│
│  │ ECUService | SensorService | VehicleService ││
│  │ DiagnosticService | ProductService          ││
│  └─────────────────────────────────────────────┘│
├─────────────────────────────────────────────────┤
│           Data Access Layer                      │
│                 (NKM.DAL)                        │
│  ┌─────────────────────────────────────────────┐│
│  │ IECURepository | ISensorRepository          ││
│  │ IVehicleRepository | IDiagnosticCodeRepo    ││
│  └─────────────────────────────────────────────┘│
├─────────────────────────────────────────────────┤
│              Entities Layer                      │
│              (NKM.Entities)                      │
│  ┌─────────────────────────────────────────────┐│
│  │ ECU | Sensor | Vehicle | DiagnosticCode     ││
│  └─────────────────────────────────────────────┘│
└─────────────────────────────────────────────────┘
```

## 📁 Proje Yapısı / Project Structure

```
N-Tier-Architecture/
├── NKM.Entities/          # Entity sınıfları
│   ├── ECU.cs             # Elektronik Kontrol Ünitesi
│   ├── Sensor.cs          # Sensör
│   ├── Vehicle.cs         # Araç
│   ├── DiagnosticCode.cs  # Arıza Kodu
│   └── Product.cs         # Ürün (orijinal)
│
├── NKM.DAL/               # Veri Erişim Katmanı
│   ├── Interfaces/        # Repository arayüzleri
│   │   ├── IECURepository.cs
│   │   ├── ISensorRepository.cs
│   │   ├── IVehicleRepository.cs
│   │   └── IDiagnosticCodeRepository.cs
│   └── Repositories/      # Repository implementasyonları
│       ├── ECURepository.cs
│       ├── SensorRepository.cs
│       ├── VehicleRepository.cs
│       └── DiagnosticCodeRepository.cs
│
├── NKM.BLL/               # İş Mantığı Katmanı
│   ├── Services/          # Servisler
│   │   ├── ECUService.cs
│   │   ├── SensorService.cs
│   │   ├── VehicleService.cs
│   │   └── DiagnosticService.cs
│   ├── ILogger.cs
│   └── Logger.cs
│
└── NKM.UI2/               # Kullanıcı Arayüzü
    └── Program.cs         # Konsol uygulaması
```

## 🔧 Gereksinimler / Requirements

- .NET Framework 4.7.2 veya üstü
- Visual Studio 2019/2022

## 🚀 Çalıştırma / Running

1. Visual Studio'da çözümü açın
2. NKM.UI2'yi başlangıç projesi olarak ayarlayın
3. F5 ile çalıştırın

## 📝 Örnek Kullanım / Example Usage

```
╔════════════════════════════════════════════════════════════╗
║     OTOMOTİV GÖMÜLÜ SİSTEM YÖNETİM UYGULAMASI              ║
║     Automotive Embedded System Management Application      ║
╚════════════════════════════════════════════════════════════╝

═══════════════════════════════════════
            ANA MENÜ / MAIN MENU
═══════════════════════════════════════
1- Araç Yönetimi (Vehicle Management)
2- ECU Yönetimi (ECU Management)
3- Sensör Yönetimi (Sensor Management)
4- Arıza Kodları (Diagnostic Codes)
5- Ürün Yönetimi (Product Management)
0- Çıkış (Exit)
```

## 🔑 Anahtar Kavramlar / Key Concepts

### OBD-II (On-Board Diagnostics)
Araç içi tanılama standardı. Arıza kodları (DTC) bu standarda göre kategorize edilir.

### ECU (Electronic Control Unit)
Modern araçlardaki elektronik kontrol üniteleri. Bir araçta 50'den fazla ECU bulunabilir.

### CAN Bus
Controller Area Network - ECU'lar arası iletişim protokolü.

### ADAS (Advanced Driver Assistance Systems)
Gelişmiş sürücü destek sistemleri - otomatik fren, şerit takip, adaptif hız sabitleme vb.

## 📄 Lisans / License

Bu proje eğitim amaçlı geliştirilmiştir.
This project is developed for educational purposes.
