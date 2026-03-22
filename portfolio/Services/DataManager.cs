using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using portfolio.Models;

namespace portfolio.Services
{
    public class DataManager
    {
        private const string DataDirectory = "Data";
        private const string PortfolioFile = "portfolio.txt";

        public DataManager()
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }

        public void SavePortfolio(Portfolio portfolio)
        {
            try
            {
                string filePath = Path.Combine(DataDirectory, PortfolioFile);
                StringBuilder sb = new StringBuilder();

                // Portfolio header
                sb.AppendLine($"PORTFOLIO_ID:{portfolio.Id}");
                sb.AppendLine($"NAME:{portfolio.Name}");
                sb.AppendLine($"DESCRIPTION:{portfolio.Description}");
                sb.AppendLine($"OWNER:{portfolio.OwnerName}");
                sb.AppendLine($"EMAIL:{portfolio.Email}");
                sb.AppendLine($"PHONE:{portfolio.Phone}");
                sb.AppendLine($"LINKEDIN:{portfolio.LinkedInUrl}");
                sb.AppendLine($"GITHUB:{portfolio.GitHubUrl}");
                sb.AppendLine($"WEBSITE:{portfolio.PersonalWebsite}");
                sb.AppendLine($"CREATED:{portfolio.CreatedDate:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"MODIFIED:{portfolio.LastModified:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine("---ITEMS---");

                // Portfolio items
                foreach (var item in portfolio.Items)
                {
                    sb.AppendLine($"ITEM_ID:{item.Id}");
                    sb.AppendLine($"TITLE:{item.Title}");
                    sb.AppendLine($"DESCRIPTION:{item.Description}");
                    sb.AppendLine($"CATEGORY:{item.Category}");
                    sb.AppendLine($"TECHNOLOGIES:{item.TechnologiesUsed}");
                    sb.AppendLine($"START_DATE:{item.StartDate:yyyy-MM-dd}");
                    sb.AppendLine($"END_DATE:{item.EndDate:yyyy-MM-dd}");
                    sb.AppendLine($"PROJECT_URL:{item.ProjectUrl}");
                    sb.AppendLine($"GITHUB_URL:{item.GitHubUrl}");
                    sb.AppendLine($"COMPLETION:{item.CompletionPercentage}");
                    sb.AppendLine($"STATUS:{item.Status}");
                    sb.AppendLine($"ITEM_CREATED:{item.CreatedDate:yyyy-MM-dd HH:mm:ss}");
                    sb.AppendLine("---END_ITEM---");
                }

                File.WriteAllText(filePath, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: Portföy kaydedilemedi. {ex.Message}");
            }
        }

        public Portfolio LoadPortfolio()
        {
            string filePath = Path.Combine(DataDirectory, PortfolioFile);
            
            if (!File.Exists(filePath))
            {
                return CreateNewPortfolio();
            }

            try
            {
                var portfolio = new Portfolio();
                var lines = File.ReadAllLines(filePath);
                var currentItem = new PortfolioItem();
                bool isReadingItem = false;

                foreach (var line in lines)
                {
                    if (line == "---ITEMS---")
                    {
                        isReadingItem = true;
                        continue;
                    }

                    if (line == "---END_ITEM---")
                    {
                        if (currentItem.Id > 0)
                        {
                            portfolio.Items.Add(currentItem);
                        }
                        currentItem = new PortfolioItem();
                        continue;
                    }

                    if (isReadingItem)
                    {
                        ParseItemLine(currentItem, line);
                    }
                    else
                    {
                        ParsePortfolioLine(portfolio, line);
                    }
                }

                return portfolio;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: Portföy yüklenemedi. {ex.Message}");
                return CreateNewPortfolio();
            }
        }

        private void ParsePortfolioLine(Portfolio portfolio, string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var parts = line.Split(new[] { ':' }, 2);
            if (parts.Length != 2) return;

            string key = parts[0];
            string value = parts[1];

            switch (key)
            {
                case "PORTFOLIO_ID": portfolio.Id = int.Parse(value); break;
                case "NAME": portfolio.Name = value; break;
                case "DESCRIPTION": portfolio.Description = value; break;
                case "OWNER": portfolio.OwnerName = value; break;
                case "EMAIL": portfolio.Email = value; break;
                case "PHONE": portfolio.Phone = value; break;
                case "LINKEDIN": portfolio.LinkedInUrl = value; break;
                case "GITHUB": portfolio.GitHubUrl = value; break;
                case "WEBSITE": portfolio.PersonalWebsite = value; break;
                case "CREATED": portfolio.CreatedDate = DateTime.Parse(value); break;
                case "MODIFIED": portfolio.LastModified = DateTime.Parse(value); break;
            }
        }

        private void ParseItemLine(PortfolioItem item, string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var parts = line.Split(new[] { ':' }, 2);
            if (parts.Length != 2) return;

            string key = parts[0];
            string value = parts[1];

            switch (key)
            {
                case "ITEM_ID": item.Id = int.Parse(value); break;
                case "TITLE": item.Title = value; break;
                case "DESCRIPTION": item.Description = value; break;
                case "CATEGORY": item.Category = value; break;
                case "TECHNOLOGIES": item.TechnologiesUsed = value; break;
                case "START_DATE": item.StartDate = DateTime.Parse(value); break;
                case "END_DATE": item.EndDate = DateTime.Parse(value); break;
                case "PROJECT_URL": item.ProjectUrl = value; break;
                case "GITHUB_URL": item.GitHubUrl = value; break;
                case "COMPLETION": item.CompletionPercentage = decimal.Parse(value); break;
                case "STATUS": item.Status = (PortfolioStatus)Enum.Parse(typeof(PortfolioStatus), value); break;
                case "ITEM_CREATED": item.CreatedDate = DateTime.Parse(value); break;
            }
        }

        private Portfolio CreateNewPortfolio()
        {
            return new Portfolio { Id = 1 };
        }

        public void DeletePortfolioFile()
        {
            string filePath = Path.Combine(DataDirectory, PortfolioFile);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
