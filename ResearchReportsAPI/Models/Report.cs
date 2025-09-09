namespace ResearchReportsAPI.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int IndustryId { get; set; }
        public string Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ReportCode { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? Keywords { get; set; }
        public string? KeyInsights { get; set; }
        public string? Toc { get; set; }
        public string? Segmentation { get; set; }
        public string? Methodology { get; set; }
        public string? StudyPeriod { get; set; }
        public string? BaseYear { get; set; }
        public string? HistoricalData { get; set; }
        public int? Pages { get; set; }
        public string? DownloadSample { get; set; }

        // Audit Fields
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Industry? Industry { get; set; }
    }
}
