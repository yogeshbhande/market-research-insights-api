
using Microsoft.EntityFrameworkCore;
using ResearchReportsAPI.Data;
using ResearchReportsAPI.Repositories.Interfaces;
using ResearchReportsAPI.Repositories;
using ResearchReportsAPI.Services;

namespace ResearchReportsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<ResearchReportsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            // Register Repositories & Services
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
            builder.Services.AddScoped<IEnquiryRepository, EnquiryRepository>();
            builder.Services.AddScoped<EnquiryService>();
            builder.Services.AddScoped<ExcelService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
