using ClosedXML.Excel;
using ResearchReportsAPI.Models;
using ResearchReportsAPI.Repositories;
using ResearchReportsAPI.Repositories.Interfaces;

public class ExcelService
{
    private readonly IReportRepository _reportRepo;
    private readonly IIndustryRepository _industryRepo;

    public ExcelService(IReportRepository reportRepo, IIndustryRepository industryRepo)
    {
        _reportRepo = reportRepo;
        _industryRepo = industryRepo;
    }

    public async Task<int> ImportReportsAsync(IFormFile[] files)
    {
        int totalInserted = 0;
        var reports = new List<Report>();

        foreach (var file in files)
        {
            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                continue;

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();

            // Read headers
            var headers = new Dictionary<string, int>();
            foreach (var cell in worksheet.Row(1).Cells())
            {
                headers[cell.Value.ToString().Trim().ToLower()] = cell.Address.ColumnNumber;
            }

            // Process rows
            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                // Check IsUploaded column
                string uploadedFlag = headers.ContainsKey("is_uploaded")
                                      ? row.Cell(headers["is_uploaded"]).GetString().Trim().ToLower()
                                      : "no";

                if (uploadedFlag == "yes" || uploadedFlag == "1" || uploadedFlag == "true")
                    continue; // already uploaded, skip

                string title = row.Cell(headers["title"]).GetString();
                if (string.IsNullOrWhiteSpace(title)) continue;

                string industryName = row.Cell(headers["industry"]).GetString();
                var industry = await _industryRepo.GetIndustryByNameAsync(industryName);
                // if industry not found, skip this row
                if (industry == null)
                    continue;

                reports.Add(new Report
                {
                    Title = title,
                    Slug = row.Cell(headers["slug"]).GetString(),
                    IndustryId = industry?.Id ?? 0,
                    Description = row.Cell(headers["description"]).GetString(),
                    Price = decimal.TryParse(row.Cell(headers["price"]).GetString(), out var p) ? p : null,
                    ReportCode = row.Cell(headers["report_code"]).GetString(),
                    MetaTitle = row.Cell(headers["meta_title"]).GetString(),
                    MetaDescription = row.Cell(headers["meta_description"]).GetString(),
                    Keywords = row.Cell(headers["keywords"]).GetString(),
                    KeyInsights = row.Cell(headers["key_insights"]).GetString(),
                    Toc = row.Cell(headers["toc"]).GetString(),
                    Segmentation = row.Cell(headers["segmentation"]).GetString(),
                    Methodology = row.Cell(headers["methodology"]).GetString(),
                    StudyPeriod = row.Cell(headers["study_period"]).GetString(),
                    BaseYear = row.Cell(headers["base_year"]).GetString(),
                    HistoricalData = row.Cell(headers["historical_data"]).GetString(),
                    Pages = int.TryParse(row.Cell(headers["pages"]).GetString(), out var pg) ? pg : null,
                    DownloadSample = row.Cell(headers["download_sample"]).GetString(),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                });
                totalInserted++;
            }
        }

        if (reports.Any())
            await _reportRepo.AddReportsAsync(reports);

        return totalInserted;
    }
}
