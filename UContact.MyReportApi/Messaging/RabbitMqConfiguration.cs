namespace UContact.MyReportApi.Messaging
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }

        public string CreateQueueName { get; set; }

        public string GeneratedQueueName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Enabled { get; set; }
    }
}
