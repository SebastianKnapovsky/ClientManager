namespace ClientManager.Core.Models
{
    public class ClientAdditionalField
    {
        public int Id { get; set; }
        public string NameField { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}
