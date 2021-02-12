using System;

namespace UContact.MyReportApi.Database.Entities
{
    public class Report : BaseEntity
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
    public enum ReportStatus
    {
        Generating = 10,
        Completed = 20
    }
}