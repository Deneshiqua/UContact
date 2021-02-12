using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UContact.MyReportApi.Database.Entities;

namespace UContact.MyReportApi.Database.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(s => s.Location).HasMaxLength(100).HasDefaultValue("");
            builder.Property(s => s.ContactCount).HasDefaultValue(0);
            builder.Property(s => s.PhoneNumberCount).HasDefaultValue(0);
            builder.Property(s => s.StatusId).HasDefaultValue(10);

            //ignore enum
            builder.Ignore(entity => entity.Status);
        }
    }
}
