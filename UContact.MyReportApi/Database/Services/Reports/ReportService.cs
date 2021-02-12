using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UContact.Common.Pager;
using UContact.MyReportApi.Database.Entities;
using UContact.MyReportApi.Messaging.Receive;

namespace UContact.MyReportApi.Database.Services
{
    public class ReportService : IReportService
    {
        private readonly MyReportDbContext _dbContext;
        private readonly IReportSender _reportSender;
        public ReportService(MyReportDbContext dbContext,
            IReportSender reportSender)
        {
            _dbContext = dbContext;
            _reportSender = reportSender;
        }

        public async Task<bool> Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var report = await _dbContext.Reports.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (report is null)
                throw new ArgumentNullException(nameof(report));

            _dbContext.Reports.Remove(report);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<PagedList<Report>> GetAll(int take = 0, int skip = 0)
        {

            var records = await _dbContext.Reports.Skip(skip).Take(take).ToListAsync();

            int recordsAllCount = _dbContext.Reports.Count();

            var paged = new PagedList<Report>(records, skip, take, recordsAllCount);

            return paged;
        }
        public async Task<Report> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var report = await _dbContext.Reports.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (report is null)
                throw new ArgumentNullException(nameof(report));

            return report;
        }
        public async Task<Report> Insert(Report entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Reports.Add(entity);
            await _dbContext.SaveChangesAsync();

            _reportSender.CreateReport(new Messaging.ReportCommand() { Location = entity.Location });
            return entity;

        }
        public async Task<Report> Update(Report entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var report = await _dbContext.Reports.FirstOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (report is null)
                throw new ArgumentNullException(nameof(report));

            _dbContext.Reports.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;

        }
    }
}
