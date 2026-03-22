# 📊 Profesyonel Portföy Yönetim Sistemi

Geliştiricilerin portföylerini kolayca yönetebileceği, modern ve profesyonel bir .NET Framework uygulaması.

## 🎯 Özellikler

- ✅ **Portföy Kurulumu** - Kişisel bilgileri, sosyal medya profillerini yönet
- ✅ **Proje Yönetimi** - Projelerinizi ekle, düzenle, sil ve kategorize et
- ✅ **İlerleme Takibi** - Her proje için tamamlanma yüzdesini takip et
- ✅ **Durum Yönetimi** - Planning, InProgress, OnHold, Completed, Archived durumları
- ✅ **Veri Kalıcılığı** - Veriler güvenli bir şekilde dosya sisteminde saklanır
- ✅ **İstatistikler** - Tamamlanan projeler, ortalama ilerleme gibi analitikler
- ✅ **Logging** - Tüm işlemlerin kayıtlarını tut
- ✅ **Profesyonel UI** - Kullanıcı dostu ve estetik arayüz

## 📋 İçindekiler

- [Kurulum](#kurulum)
- [Kullanım](#kullanım)
- [Proje Yapısı](#proje-yapısı)
- [Sistem Gereksinimleri](#sistem-gereksinimleri)
- [Geliştirme](#geliştirme)
- [Lisans](#lisans)

## 🚀 Kurulum

### Sistem Gereksinimleri

- .NET Framework 4.7.2 veya üzeri
- Windows 7 SP1 veya üzeri
- 50 MB boş disk alanı

### Adımlar

1. Projeyi klonla:
```bash
git clone https://github.com/tah4s/CSharpDone.git
cd portfolio
```

2. Visual Studio ile aç:
```bash
start portfolio.sln
```

3. Build ve Çalıştır:
```
Ctrl + Shift + B  (Build)
F5               (Run)
```

## 💻 Kullanım

### Ana Menü

Uygulama başlatıldığında aşağıdaki menü karşınıza gelir:

```
ANA MENU
-----------
1. Portföyü Kur
2. Yeni Proje Ekle
3. Tüm Projeleri Görüntüle
4. Projeyi Güncelle
5. Projeyi Sil
6. Portföy Özeti
0. Çıkış
```

### Senaryo: İlk Çalıştırma

1. **Portföyü Kur (1)** seçeneğini seç
2. Kişisel bilgilerini gir (Ad, Email, GitHub vb.)
3. **Yeni Proje Ekle (2)** ile projelerinizi ekle
4. **Tüm Projeleri Görüntüle (3)** ile kontrolünü sağla
5. **Portföy Özeti (6)** ile istatistikleri gör

### Veri Depolama

Tüm veriler şu konumda saklanır:
```
.\Data\portfolio.txt
```

Bu dosyayı yedekleyebilir veya yer değiştirebilirsiniz.

## 📁 Proje Yapısı

```
portfolio/
├── Models/
│   ├── Portfolio.cs          # Portföy ana modeli
│   └── PortfolioItem.cs      # Proje/Item modeli
├── Services/
│   ├── PortfolioService.cs   # İş mantığı
│   ├── DataManager.cs        # Veri yönetimi
│   ├── ConfigManager.cs      # Konfigürasyon
│   └── Logger.cs             # Logging sistemi
├── UI/
│   └── MenuManager.cs        # Kullanıcı arayüzü
├── Data/                     # Veri klasörü (otomatik oluşturulur)
├── Program.cs                # Entry point
├── App.config                # Uygulama konfigürasyonu
└── README.md                 # Bu dosya
```

## 🔧 Geliştirme

### Eklenebilecek Özellikler

- [ ] SQL Server / SQLite entegrasyonu
- [ ] Web API versiyonu (ASP.NET Core)
- [ ] WPF/WinForms arayüzü
- [ ] Proje şablonları
- [ ] Çoklu kullanıcı desteği
- [ ] Cloud senkronizasyonu (Azure/OneDrive)
- [ ] Export (PDF, HTML, JSON)
- [ ] Ağırlık/Öncelik sistemi

### Hata Bildirme

Herhangi bir hata bulduysanız:
1. GitHub Issues'i kontrol et
2. Yeni bir issue açarak bildir

### Katkı Sağlama

1. Fork et
2. Feature branch oluştur (`git checkout -b feature/YeniOzellik`)
3. Commit yap (`git commit -m 'Yeni özellik ekle'`)
4. Push et (`git push origin feature/YeniOzellik`)
5. Pull Request aç

## 📊 Sistem Mimarisi

### Katmanlı Mimari

```
Presentation Layer (UI)
        ↓
Business Logic Layer (Services)
        ↓
Data Access Layer (DataManager)
        ↓
Data Layer (Files)
```

### Veri Akışı

```
Menu → MenuManager → PortfolioService → DataManager → File System
```

## 🔐 Güvenlik Notları

- Veriler şifreli olarak saklanmaz (hassas olmayan veri)
- Dosya izinlerini kontrol edin
- Portföy dosyasını yedekleyin

## 📝 Günlükleme

Tüm işlemlerin logları tutulur:
```
.\Logs\portfolio_YYYY-MM-DD.log
```

## 📄 Lisans

Bu proje MIT Lisansı altında sunulmaktadır. Detaylar için LICENSE dosyasını kontrol edin.

## 👨‍💻 Geliştirici

**Tahas** - [GitHub](https://github.com/tah4s)

## 📞 İletişim

- Email: [Email adresiniz]
- GitHub Issues: [Proje Issues Sayfası]
- Twitter: [@YourHandle]

## 🙏 Teşekkürler

Bu proje aşağıdaki kaynakların ilham alınarak geliştirilmiştir:
- .NET Framework Best Practices
- Clean Code Principles
- Design Patterns

## 📈 Yol Haritası

- **v1.0** (Mevcut) - Temel özellikler
- **v1.1** - Logging sistemi, geliştirilmiş UI
- **v2.0** - Veritabanı entegrasyonu
- **v3.0** - Web API ve mobil uygulaması

---

**Son Güncelleme:** 2026-03-22
