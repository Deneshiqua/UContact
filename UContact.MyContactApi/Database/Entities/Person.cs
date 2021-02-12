using System;
using System.Collections.Generic;

namespace UContact.MyContactApi.Database.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreatedOn { get; set; }

        public List<PersonInfo> Infos { get; set; } = new List<PersonInfo>();
    }
}
