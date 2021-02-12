using Microsoft.EntityFrameworkCore;
using UContact.MyReportApi.Database.Configurations;
using UContact.MyReportApi.Database.Entities;

namespace UContact.MyReportApi.Database
{
    public class MyReportDbContext : DbContext
    {
        public MyReportDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
        }

        public DbSet<Report> Reports { get; set; }
    }
}
