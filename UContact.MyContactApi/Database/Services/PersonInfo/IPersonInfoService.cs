using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UContact.Common.Pager;
using UContact.MyContactApi.Database.Entities;

namespace UContact.MyContactApi.Database.Services
{
    public interface IPersonInfoService
    {
        Task<List<PersonInfo>> GetAll(Guid personId, int take = 0, int skip = 0);
        Task<PersonInfo> GetById(Guid id);
        Task<PersonInfo> Insert(PersonInfo personInfo);
        Task<PersonInfo> Update(PersonInfo personInfo);
        Task<bool> Delete(Guid id);
    }
}
