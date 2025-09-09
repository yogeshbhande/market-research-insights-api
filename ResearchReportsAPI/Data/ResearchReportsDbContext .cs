using Microsoft.EntityFrameworkCore;
using ResearchReportsAPI.Models;

namespace ResearchReportsAPI.Data
{
    public class ResearchReportsDbContext : DbContext
    {
        public ResearchReportsDbContext(DbContextOptions<ResearchReportsDbContext> option) : base(option)
        {

        }

        public DbSet<Industry> Industries { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Enquiry> Enquiry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Industry>(entity =>
            {
                entity.ToTable("Industries");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.IndustryName)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.HasIndex(e => e.IndustryName)
                      .IsUnique();

                entity.Property(e => e.CreatedBy)
                      .HasMaxLength(100)
                      .HasDefaultValueSql("suser_sname()");

                entity.Property(e => e.CreatedDate)
                      .HasDefaultValueSql("sysutcdatetime()");

                entity.Property(e => e.ModifiedBy)
                      .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                      .HasDefaultValue(true);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Reports");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).HasMaxLength(500).IsRequired();
                entity.HasIndex(e => e.Title);

                entity.HasOne(r => r.Industry)
                      .WithMany()
                      .HasForeignKey(r => r.IndustryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
