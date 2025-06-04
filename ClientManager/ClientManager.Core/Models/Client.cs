namespace ClientManager.Core.Models
{
    public class Client
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string NIP { get; set; } = string.Empty;

        public List<ClientAdditionalField>? AdditionalFields { get; set; }
    }
}
