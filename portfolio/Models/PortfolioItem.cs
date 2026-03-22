using System;

namespace portfolio.Models
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string TechnologiesUsed { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectUrl { get; set; }
        public string GitHubUrl { get; set; }
        public decimal CompletionPercentage { get; set; }
        public PortfolioStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public PortfolioItem()
        {
            CreatedDate = DateTime.Now;
            Status = PortfolioStatus.InProgress;
            CompletionPercentage = 0;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title} - {Category} ({Status}) - {CompletionPercentage}%";
        }
    }

    public enum PortfolioStatus
    {
        Planning = 0,
        InProgress = 1,
        OnHold = 2,
        Completed = 3,
        Archived = 4
    }
}
