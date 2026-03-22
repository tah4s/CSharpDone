# Git Konfigürasyonu

Bu dosya proje kurulum sonrasında silinebilir.

## GitHub'a Push Etmek İçin:

```bash
# 1. Repo'ya gir
cd C:\Users\Administrator\source\repos\C#Done

# 2. Git konfigürasyonunu kontrol et
git config user.name
git config user.email

# 3. Tüm dosyaları stage et
git add .

# 4. Commit yap
git commit -m "Professional Portfolio Management System v1.0

- Complete CRUD operations for portfolio items
- Logging and configuration management
- Data persistence with file system
- Professional CLI UI with Turkish language support
- Comprehensive documentation and README
- MIT License"

# 5. GitHub'a push et
git push origin master
```

## Branch Stratejisi

```
master
├── develop
└── features/*
```

## Commit Mesajları

Türkçe veya İngilizce olabilir:
- ✨ Yeni özellik: `feat: Proje sayfası eklendi`
- 🐛 Bug fix: `fix: Tarih parse hatası düzeltildi`
- 📝 Documentation: `docs: README güncellenildi`
- 🔧 Config: `chore: App.config güncellemesi`
