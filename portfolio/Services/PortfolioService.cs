using System;
using System.Linq;
using portfolio.Models;

namespace portfolio.Services
{
    public class PortfolioService
    {
        private Portfolio _portfolio;
        private DataManager _dataManager;

        public PortfolioService()
        {
            _dataManager = new DataManager();
            _portfolio = _dataManager.LoadPortfolio();
        }

        public Portfolio GetPortfolio()
        {
            return _portfolio;
        }

        public void InitializePortfolio(string name, string ownerName, string email)
        {
            _portfolio.Name = name;
            _portfolio.OwnerName = ownerName;
            _portfolio.Email = email;
            SavePortfolio();
        }

        public void AddPortfolioItem(string title, string description, string category, 
            string technologies, DateTime startDate, DateTime? endDate = null)
        {
            var newId = _portfolio.Items.Count > 0 ? _portfolio.Items.Max(x => x.Id) + 1 : 1;

            var item = new PortfolioItem
            {
                Id = newId,
                Title = title,
                Description = description,
                Category = category,
                TechnologiesUsed = technologies,
                StartDate = startDate,
                EndDate = endDate ?? DateTime.MaxValue
            };

            _portfolio.Items.Add(item);
            _portfolio.UpdateModifiedDate();
            SavePortfolio();
        }

        public void UpdatePortfolioItem(int itemId, string title = null, string description = null,
            decimal? completion = null, PortfolioStatus? status = null)
        {
            var item = _portfolio.Items.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
            {
                Console.WriteLine("Proje bulunamadı!");
                return;
            }

            if (!string.IsNullOrEmpty(title)) item.Title = title;
            if (!string.IsNullOrEmpty(description)) item.Description = description;
            if (completion.HasValue) item.CompletionPercentage = completion.Value;
            if (status.HasValue) item.Status = status.Value;

            _portfolio.UpdateModifiedDate();
            SavePortfolio();
        }

        public void DeletePortfolioItem(int itemId)
        {
            var item = _portfolio.Items.FirstOrDefault(x => x.Id == itemId);
            if (item != null)
            {
                _portfolio.Items.Remove(item);
                _portfolio.UpdateModifiedDate();
                SavePortfolio();
            }
        }

        public void SavePortfolio()
        {
            _dataManager.SavePortfolio(_portfolio);
        }

        public void DisplayPortfolioSummary()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("PORTFÖY ÖZETI");
            Console.WriteLine(new string('=', 60));
            
            if (string.IsNullOrEmpty(_portfolio.OwnerName))
            {
                Console.WriteLine("Portföy henüz başlatılmadı. Lütfen önce kurun.");
                return;
            }

            Console.WriteLine($"Ad: {_portfolio.Name}");
            Console.WriteLine($"Sahip: {_portfolio.OwnerName}");
            Console.WriteLine($"Email: {_portfolio.Email}");
            Console.WriteLine($"Telefon: {_portfolio.Phone}");
            Console.WriteLine($"GitHub: {_portfolio.GitHubUrl}");
            Console.WriteLine($"Web Sitesi: {_portfolio.PersonalWebsite}");
            Console.WriteLine($"\nToplam Proje: {_portfolio.Items.Count}");
            Console.WriteLine($"Tamamlanan: {_portfolio.GetCompletedProjectCount()}");
            Console.WriteLine($"Devam Eden: {_portfolio.GetInProgressProjectCount()}");
            Console.WriteLine($"Ortalama Tamamlanma: {_portfolio.GetAverageCompletion():F1}%");
            Console.WriteLine($"Son Güncelleme: {_portfolio.LastModified:dd.MM.yyyy HH:mm}");
            Console.WriteLine(new string('=', 60));
        }

        public void DisplayAllItems()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("PORTFÖY PROJELERİ");
            Console.WriteLine(new string('=', 60));

            if (_portfolio.Items.Count == 0)
            {
                Console.WriteLine("Henüz proje eklenmemiş.");
                return;
            }

            foreach (var item in _portfolio.Items.OrderBy(x => x.Status))
            {
                DisplayItemDetails(item);
            }

            Console.WriteLine(new string('=', 60));
        }

        public void DisplayItemDetails(PortfolioItem item)
        {
            Console.WriteLine($"\n[{item.Id}] {item.Title}");
            Console.WriteLine($"   Kategori: {item.Category}");
            Console.WriteLine($"   Açıklama: {item.Description}");
            Console.WriteLine($"   Teknolojiler: {item.TechnologiesUsed}");
            Console.WriteLine($"   Durumu: {item.Status}");
            Console.WriteLine($"   Tamamlanma: {item.CompletionPercentage}%");
            Console.WriteLine($"   Başlangıç: {item.StartDate:dd.MM.yyyy}");
            if (item.EndDate != DateTime.MaxValue)
            {
                Console.WriteLine($"   Bitiş: {item.EndDate:dd.MM.yyyy}");
            }
            if (!string.IsNullOrEmpty(item.GitHubUrl))
            {
                Console.WriteLine($"   GitHub: {item.GitHubUrl}");
            }
            if (!string.IsNullOrEmpty(item.ProjectUrl))
            {
                Console.WriteLine($"   Proje URL: {item.ProjectUrl}");
            }
        }
    }
}
