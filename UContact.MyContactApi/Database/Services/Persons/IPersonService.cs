using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UContact.Common.Pager;
using UContact.MyContactApi.Database.Entities;
using UContact.MyContactApi.Messaging;

namespace UContact.MyContactApi.Database.Services
{
    public interface IPersonService
    {
        Task<IPagedList<Person>> GetAll(int take = 0, int skip = 0);
        Task<ReportModel> GetReport(ReportCommand reportCommand);
        Task<Person> GetById(Guid id);
        Task<Person> Insert(Person person);
        Task<Person> Update(Person person);
        Task<bool> Delete(Guid id);
    }
}
