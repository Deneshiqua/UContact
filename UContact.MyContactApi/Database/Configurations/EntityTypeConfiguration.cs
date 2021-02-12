using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UContact.MyContactApi.Database.Entities;

namespace UContact.MyContactApi.Database.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(s => s.Name).HasMaxLength(100).HasDefaultValue("");
            builder.Property(s => s.Surname).HasMaxLength(100).HasDefaultValue("");
            builder.Property(s => s.CompanyName).HasMaxLength(200).HasDefaultValue("");

            builder.HasMany(entity => entity.Infos)
                .WithOne(entity => entity.Person);
        }
    }
    public class PersonInfoConfiguration : IEntityTypeConfiguration<PersonInfo>
    {
        public void Configure(EntityTypeBuilder<PersonInfo> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(s => s.Value).HasMaxLength(100).HasDefaultValue("");

            //ignore enum
            builder.Ignore(entity => entity.InfoType);

            builder.HasOne(entity => entity.Person)
                .WithMany(entity => entity.Infos);
        }
    }
}
