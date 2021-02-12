using System;
using System.Collections.Generic;
using UContact.MyContactApi.Database.Entities;

namespace UContact.MyContactApi.Models
{
    public class PersonModel : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreatedOn { get; set; }

        public List<PersonInfoModel> Infos { get; set; } = new List<PersonInfoModel>();
    }
}
