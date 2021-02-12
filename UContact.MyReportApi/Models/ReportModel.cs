using System;
using UContact.MyReportApi.Database.Entities;

namespace UContact.MyReportApi.Models
{
    public class ReportModel : BaseEntity
    {
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }

        public ReportStatus Status
        {
            get => (ReportStatus)StatusId;
            set => StatusId = (int)value;
        }
    }
}