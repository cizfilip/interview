namespace Lego.Mails
{
    public class MailOptions
    {
        public string Server { get; set; }

        public int Port { get; set; }

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool UsePickupFolder { get; set; }

        public string PickupFolder { get; set; }
    }
}
