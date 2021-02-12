using System;
using System.Threading.Tasks;
using UContact.Common.Pager;
using UContact.MyReportApi.Database.Entities;

namespace UContact.MyReportApi.Database.Services
{
    public interface IReportService
    {
        Task<PagedList<Report>> GetAll(int take = 0, int skip = 0);
        Task<Report> GetById(Guid id);
        Task<Report> Insert(Report person);
        Task<Report> Update(Report person);
        Task<bool> Delete(Guid id);
    }
}
