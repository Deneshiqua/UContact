using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UContact.MyContactApi.Database.Entities;
using UContact.MyContactApi.Database.Services;
using UContact.MyContactApi.Messaging.Send;
using Xunit;

namespace UContact.MyContactAp.Test
{
    public class PersonTest : TestBase
    {
        [Fact]
        public async Task CreatePerson()
        {
            var person = new Person
            {
                Name = "Deniz",
                Surname = "Yýldýz",
                CompanyName = "MyCompany",
                Infos = new List<PersonInfo>
                {
                    new PersonInfo{ InfoType = PeopleInfoType.Phone, Value = "+905554587069" },
                    new PersonInfo{ InfoType = PeopleInfoType.Email, Value = "deniz@yildiz.com" },
                    new PersonInfo{ InfoType = PeopleInfoType.Location, Value = "Izmir" }
                }
            };

            var reportSender = new Mock<IReportSender>();
            var handler = new Mock<PersonService>(CreateDbContext(), reportSender.Object);
            var createModel = await handler.Object.Insert(person);

            Assert.NotEqual(Guid.Empty, createModel.Id);
            Assert.NotEqual(DateTime.MinValue, createModel.CreatedOn);
        }
        [Fact]
        public async Task DeletePerson()
        {
            var person = new Person
            {
                Name = "Deniz",
                Surname = "Yýldýz",
                CompanyName = "MyCompany",
                Infos = new List<PersonInfo>
                {
                    new PersonInfo{ InfoType = PeopleInfoType.Phone, Value = "+905554587069" },
                    new PersonInfo{ InfoType = PeopleInfoType.Email, Value = "deniz@yildiz.com" },
                    new PersonInfo{ InfoType = PeopleInfoType.Location, Value = "Izmir" }
                }
            };

            var reportSender = new Mock<IReportSender>();
            var handler = new Mock<PersonService>(CreateDbContext(), reportSender.Object);
            var personEntity = await handler.Object.Insert(person);
            
            var delete = await handler.Object.Delete(personEntity.Id);
            Assert.True(delete);

        }
    }
}
