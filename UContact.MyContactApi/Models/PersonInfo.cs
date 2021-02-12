using System;
using UContact.MyContactApi.Database.Entities;

namespace UContact.MyContactApi.Models
{
    public class PersonInfoModel : BaseEntity
    {
        public string Value { get; set; }
        public int InfoTypeId { get; set; }

        public PeopleInfoType InfoType
        {
            get => (PeopleInfoType)InfoTypeId;
            set => InfoTypeId = (int)value;
        }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }

    public enum PeopleInfoType
    {
        Phone = 10,
        Email = 20,
        Location = 30
    }
}
