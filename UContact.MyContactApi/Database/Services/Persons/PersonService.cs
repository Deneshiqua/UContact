using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UContact.Common.Pager;
using UContact.MyContactApi.Database.Entities;
using UContact.MyContactApi.Messaging;
using UContact.MyContactApi.Messaging.Send;

namespace UContact.MyContactApi.Database.Services
{
    public class PersonService : IPersonService
    {
        private readonly MyContactDbContext _dbContext;
        private readonly IReportSender _reportSender;
        public PersonService(MyContactDbContext dbContext,
            IReportSender reportSender)
        {
            this._dbContext = dbContext;
            this._reportSender = reportSender;
        }

        public async Task<bool> Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var person = await _dbContext.Persons.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (person is null)
                throw new ArgumentNullException(nameof(person));

            _dbContext.Persons.Remove(person);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<IPagedList<Person>> GetAll(int take = 10, int skip = 0)
        {
            IQueryable<Person> recordsFiltered = _dbContext.Persons.Include(o => o.Infos);

            int recordsFilteredCount = await recordsFiltered.CountAsync();

            var records = await recordsFiltered.OrderBy(o => o.CreatedOn).Skip(skip).Take(take).ToListAsync();

            return new PagedList<Person>(records, skip, take, recordsFilteredCount);
        }
        public async Task<ReportModel> GetReport(ReportCommand reportCommand)
        {
            IQueryable<Person> recordsFiltered = _dbContext.Persons.Include(x => x.Infos)
                .Where(x => x.Infos.Any(info => info.InfoTypeId == (int)PeopleInfoType.Location && info.Value.Equals(reportCommand.Location)));

            var contactCount = await recordsFiltered.CountAsync();
            var phoneNumberCount = await recordsFiltered.SumAsync(x => x.Infos.Count(info => info.InfoTypeId == (int)PeopleInfoType.Phone));

            var model = new ReportModel
            {
                ContactCount = contactCount,
                PhoneNumberCount = phoneNumberCount,
                Location = reportCommand.Location
            };

            _reportSender.ReportCompleted(model);

            return model;
        }
        public async Task<Person> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            IQueryable<Person> recordsFiltered = _dbContext.Persons.Include(o => o.Infos);

            var person = await recordsFiltered.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (person is null)
                throw new ArgumentNullException(nameof(person));

            return person;
        }
        public async Task<Person> Insert(Person entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Persons.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;

        }
        public async Task<Person> Update(Person entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var person = await _dbContext.Persons.FirstOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (person is null)
                throw new ArgumentNullException(nameof(person));

            _dbContext.Persons.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;

        }
    }
}
