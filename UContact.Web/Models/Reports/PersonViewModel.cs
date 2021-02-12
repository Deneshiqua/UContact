using System;

namespace UContact.Web.Models.Reports
{
    public class ReportViewModel : BaseModel
    {
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
