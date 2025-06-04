using System.ComponentModel.DataAnnotations;

namespace ClientManager.Core.DTOs
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [MaxLength(20)]
        public string NIP { get; set; } = string.Empty;
        public List<ClientAdditionalFieldDto>? AdditionalFields { get; set; } = new();
    }
}
