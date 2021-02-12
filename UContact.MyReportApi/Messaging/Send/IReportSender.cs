using UContact.MyReportApi.Database.Entities;

namespace UContact.MyReportApi.Messaging.Receive
{
    public interface IReportSender
    {
        void CreateReport(ReportCommand reportCommand);
    }
}
