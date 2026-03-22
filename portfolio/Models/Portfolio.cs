using System;
using System.Collections.Generic;
using System.Linq;

namespace portfolio.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string PersonalWebsite { get; set; }
        public List<PortfolioItem> Items { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }

        public Portfolio()
        {
            Items = new List<PortfolioItem>();
            CreatedDate = DateTime.Now;
            LastModified = DateTime.Now;
        }

        public int GetCompletedProjectCount()
        {
            return Items.Count(x => x.Status == PortfolioStatus.Completed);
        }

        public int GetInProgressProjectCount()
        {
            return Items.Count(x => x.Status == PortfolioStatus.InProgress);
        }

        public decimal GetAverageCompletion()
        {
            if (Items.Count == 0) return 0;
            return Items.Average(x => x.CompletionPercentage);
        }

        public void UpdateModifiedDate()
        {
            LastModified = DateTime.Now;
        }
    }
}
