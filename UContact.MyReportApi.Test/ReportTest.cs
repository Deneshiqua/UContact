using Moq;
using System;
using System.Threading.Tasks;
using UContact.MyReportApi.Database.Entities;
using UContact.MyReportApi.Database.Services;
using UContact.MyReportApi.Messaging.Receive;
using Xunit;

namespace UContact.MyReportApi.Test
{
    public class ReportTest : TestBase
    {

        [Fact]
        public async Task CreateReport()
        {
            var person = new Report
            {
                CreatedOn = DateTime.Now,
                Location = "Ankara",
                PhoneNumberCount = 0,
                Status = ReportStatus.Generating,
                ContactCount = 0
            };

            var reportSender = new Mock<IReportSender>();
            var handler = new Mock<ReportService>(CreateDbContext(), reportSender.Object);
            var createModel = await handler.Object.Insert(person);

            Assert.NotEqual(Guid.Empty, createModel.Id);
            Assert.NotEqual(DateTime.MinValue, createModel.CreatedOn);
        }
        [Fact]
        public async Task UpdateReport()
        {
            var report = new Report
            {
                CreatedOn = DateTime.Now,
                Location = "Ankara",
                PhoneNumberCount = 0,
                Status = ReportStatus.Generating,
                ContactCount = 0
            };

            var reportSender = new Mock<IReportSender>();
            var handler = new Mock<ReportService>(CreateDbContext(), reportSender.Object);
            var reportEntity = await handler.Object.Insert(report);

            reportEntity.PhoneNumberCount = 10;
            reportEntity.ContactCount = 10;
            reportEntity.Status = ReportStatus.Completed;

            var update = await handler.Object.Update(reportEntity);
            Assert.Equal(reportEntity.Id, update.Id);

        }
    }
}
