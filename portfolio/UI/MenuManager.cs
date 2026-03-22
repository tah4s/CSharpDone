using System;
using portfolio.Models;
using portfolio.Services;

namespace portfolio.UI
{
    public class MenuManager
    {
        private PortfolioService _service;

        public MenuManager()
        {
            _service = new PortfolioService();
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║    PROFESYONEİ PORTFÖY YÖNETİM SİSTEMİ v1.0           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            bool isRunning = true;
            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SetupPortfolio();
                        break;
                    case "2":
                        AddNewProject();
                        break;
                    case "3":
                        ViewAllProjects();
                        break;
                    case "4":
                        UpdateProject();
                        break;
                    case "5":
                        DeleteProject();
                        break;
                    case "6":
                        ViewPortfolioSummary();
                        break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine("\nÇıkılıyor...");
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }

                if (isRunning && choice != "3" && choice != "6")
                {
                    PressAnyKey();
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("ANA MENU");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("1. Portföyü Kur");
            Console.WriteLine("2. Yeni Proje Ekle");
            Console.WriteLine("3. Tüm Projeleri Görüntüle");
            Console.WriteLine("4. Projeyi Güncelle");
            Console.WriteLine("5. Projeyi Sil");
            Console.WriteLine("6. Portföy Özeti");
            Console.WriteLine("0. Çıkış");
            Console.WriteLine(new string('-', 60));
            Console.Write("Seçiminiz: ");
        }

        private void SetupPortfolio()
        {
            Console.Clear();
            Console.WriteLine("PORTFÖY KURULUMU");
            Console.WriteLine(new string('=', 60));

            Console.Write("Portföy Adı: ");
            string name = Console.ReadLine();

            Console.Write("Adınız: ");
            string ownerName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Telefon: ");
            string phone = Console.ReadLine();

            Console.Write("GitHub Profili: ");
            string github = Console.ReadLine();

            Console.Write("Web Sitesi: ");
            string website = Console.ReadLine();

            Console.Write("LinkedIn Profili: ");
            string linkedin = Console.ReadLine();

            _service.InitializePortfolio(name, ownerName, email);
            var portfolio = _service.GetPortfolio();
            portfolio.Phone = phone;
            portfolio.GitHubUrl = github;
            portfolio.PersonalWebsite = website;
            portfolio.LinkedInUrl = linkedin;
            portfolio.UpdateModifiedDate();
            _service.SavePortfolio();

            Console.WriteLine("\n✓ Portföy başarıyla kuruldu!");
        }

        private void AddNewProject()
        {
            Console.Clear();
            Console.WriteLine("YENİ PROJE EKLE");
            Console.WriteLine(new string('=', 60));

            Console.Write("Proje Adı: ");
            string title = Console.ReadLine();

            Console.Write("Açıklama: ");
            string description = Console.ReadLine();

            Console.Write("Kategori (Web/Masaüstü/Mobil/vb): ");
            string category = Console.ReadLine();

            Console.Write("Kullanılan Teknolojiler: ");
            string technologies = Console.ReadLine();

            Console.Write("Başlangıç Tarihi (yyyy-MM-dd): ");
            DateTime startDate = ParseDate(Console.ReadLine());

            Console.Write("GitHub URL (Boş geçebilirsiniz): ");
            string github = Console.ReadLine();

            Console.Write("Proje URL (Boş geçebilirsiniz): ");
            string projectUrl = Console.ReadLine();

            _service.AddPortfolioItem(title, description, category, technologies, startDate);
            
            // Update additional fields
            var portfolio = _service.GetPortfolio();
            var lastItem = portfolio.Items[portfolio.Items.Count - 1];
            lastItem.GitHubUrl = github;
            lastItem.ProjectUrl = projectUrl;
            _service.SavePortfolio();

            Console.WriteLine("\n✓ Proje başarıyla eklendi!");
        }

        private void ViewAllProjects()
        {
            _service.DisplayAllItems();
            PressAnyKey();
        }

        private void UpdateProject()
        {
            Console.Clear();
            _service.DisplayAllItems();

            Console.Write("\nGüncellenecek Proje ID'si: ");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Geçersiz ID!");
                return;
            }

            var portfolio = _service.GetPortfolio();
            var item = portfolio.Items.Find(x => x.Id == itemId);
            if (item == null)
            {
                Console.WriteLine("Proje bulunamadı!");
                return;
            }

            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("PROJE GÜNCELLE");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("1. Başlık");
            Console.WriteLine("2. Açıklama");
            Console.WriteLine("3. Tamamlanma Yüzdesi");
            Console.WriteLine("4. Durumu");
            Console.WriteLine("5. Teknolojiler");
            Console.WriteLine("6. Bitiş Tarihi");
            Console.Write("Seçiminiz: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Yeni Başlık: ");
                    _service.UpdatePortfolioItem(itemId, title: Console.ReadLine());
                    break;
                case "2":
                    Console.Write("Yeni Açıklama: ");
                    _service.UpdatePortfolioItem(itemId, description: Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Tamamlanma Yüzdesi (0-100): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal completion))
                    {
                        _service.UpdatePortfolioItem(itemId, completion: completion);
                    }
                    break;
                case "4":
                    Console.WriteLine("Durumlar:");
                    foreach (PortfolioStatus status in Enum.GetValues(typeof(PortfolioStatus)))
                    {
                        Console.WriteLine($"  {(int)status}: {status}");
                    }
                    Console.Write("Seçiminiz: ");
                    if (int.TryParse(Console.ReadLine(), out int statusValue))
                    {
                        _service.UpdatePortfolioItem(itemId, status: (PortfolioStatus)statusValue);
                    }
                    break;
                case "5":
                    Console.Write("Yeni Teknolojiler: ");
                    item.TechnologiesUsed = Console.ReadLine();
                    _service.SavePortfolio();
                    break;
                case "6":
                    Console.Write("Bitiş Tarihi (yyyy-MM-dd): ");
                    item.EndDate = ParseDate(Console.ReadLine());
                    _service.SavePortfolio();
                    break;
            }

            Console.WriteLine("✓ Proje güncellendi!");
        }

        private void DeleteProject()
        {
            Console.Clear();
            _service.DisplayAllItems();

            Console.Write("\nSilinecek Proje ID'si: ");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Geçersiz ID!");
                return;
            }

            Console.Write("Emin misiniz? (E/H): ");
            if (Console.ReadLine().ToUpper() == "E")
            {
                _service.DeletePortfolioItem(itemId);
                Console.WriteLine("✓ Proje silindi!");
            }
        }

        private void ViewPortfolioSummary()
        {
            _service.DisplayPortfolioSummary();
            PressAnyKey();
        }

        private void PressAnyKey()
        {
            Console.WriteLine("\nDevam etmek için herhangi bir tuşa basın...");
            Console.ReadKey();
            Console.Clear();
        }

        private DateTime ParseDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime result))
            {
                return result;
            }
            return DateTime.Now;
        }
    }
}
