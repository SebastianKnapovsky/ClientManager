using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Core.DTOs
{
    public class ClientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string NIP { get; set; } = string.Empty;
        public Dictionary<string, string>? AdditionalFields { get; set; }
    }
}
