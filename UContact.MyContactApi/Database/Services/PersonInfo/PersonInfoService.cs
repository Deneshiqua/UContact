using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UContact.MyContactApi.Database.Entities;

namespace UContact.MyContactApi.Database.Services
{
    public class PersonInfoService : IPersonInfoService
    {
        private readonly MyContactDbContext _dbContext;
        public PersonInfoService(MyContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var personInfo = await _dbContext.PersonInfos.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (personInfo is null)
                throw new ArgumentNullException(nameof(personInfo));

            _dbContext.PersonInfos.Remove(personInfo);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<List<PersonInfo>> GetAll(Guid personId, int take = 10, int skip = 0)
        {

            var records = await _dbContext.PersonInfos.Where(o => o.PersonId == personId).ToListAsync();

            var paged = records;

            return paged;
        }
        public async Task<PersonInfo> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var personInfo = await _dbContext.PersonInfos.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (personInfo is null)
                throw new ArgumentNullException(nameof(personInfo));

            return personInfo;
        }
        public async Task<PersonInfo> Insert(PersonInfo entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.PersonInfos.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;

        }
        public async Task<PersonInfo> Update(PersonInfo entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var personInfo = await _dbContext.PersonInfos.FirstOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (personInfo is null)
                throw new ArgumentNullException(nameof(personInfo));

            _dbContext.PersonInfos.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;

        }
    }
}
