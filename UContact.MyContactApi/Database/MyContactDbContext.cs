using Microsoft.EntityFrameworkCore;
using UContact.MyContactApi.Database.Configurations;
using UContact.MyContactApi.Database.Entities;

namespace UContact.MyContactApi.Database
{
    public class MyContactDbContext : DbContext
    {
        public MyContactDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonInfoConfiguration());
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
    }
}
