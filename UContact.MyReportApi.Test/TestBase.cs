using Microsoft.EntityFrameworkCore;
using System;
using UContact.MyReportApi.Database;

namespace UContact.MyReportApi.Test
{
    public class TestBase
    {
        protected static MyReportDbContext CreateDbContext(string dbName = "MyReportDb")
        {
            var context = new MyReportDbContext(new DbContextOptionsBuilder<MyReportDbContext>()
            .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
            .Options);

            return context;
        }
    }
}
