namespace CryptoPlayground.Domain.Models
{
    public class ClientKey
    {
        public Guid ClientId { get; set; }
        public Guid ApiKeyId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string Key { get; set; }

    }
}