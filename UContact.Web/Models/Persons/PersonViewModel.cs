using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UContact.Web.Models.Persons
{
    public class PersonViewModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreatedOn { get; set; }

        public List<PersonInfoModel> Infos { get; set; } = new List<PersonInfoModel>();

        [JsonIgnore]
        public List<SelectListItem> InfoTypes { get; set; } = new List<SelectListItem>();

        [JsonIgnore]
        public List<SelectListItem> Locations { get; set; } = new List<SelectListItem>();

        public class PersonInfoModel
        {
            public Guid? Id { get; set; }
            public Guid? PersonId { get; set; }
            public string Value { get; set; }
        }
    }
}
