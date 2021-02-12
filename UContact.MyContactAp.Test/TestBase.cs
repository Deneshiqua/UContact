using Microsoft.EntityFrameworkCore;
using System;
using UContact.MyContactApi.Database;

namespace UContact.MyContactAp.Test
{
    public class TestBase
    {
        protected static MyContactDbContext CreateDbContext(string dbName = "MyContactDb")
        {
            var context = new MyContactDbContext(new DbContextOptionsBuilder<MyContactDbContext>()
            .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
            .Options);

            return context;
        }
    }
}
