using System.Net.Mail;

namespace DomainArchitecture.Infrastructure.Email {

    public interface IEmailClient : IDisposable {
        void SendEmail(string recipient, string subject, string body);
    }

    public class EmailClient : IEmailClient {
        private readonly SmtpClient _client;
        private readonly string _from;

        public EmailClient(string from, string host, int port) {
            _client = new SmtpClient(host, port);
            _from = from;
        }

        public void Dispose() {
            _client.Dispose();
        }

        public void SendEmail(string recipient, string subject, string body) {
            _client.Send(_from, recipient, subject, body);
        }
    }
}