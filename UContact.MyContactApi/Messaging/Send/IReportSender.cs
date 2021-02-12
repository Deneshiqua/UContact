namespace UContact.MyContactApi.Messaging.Send
{
    public interface IReportSender
    {
        void ReportCompleted(ReportModel reportModel);
    }
}
