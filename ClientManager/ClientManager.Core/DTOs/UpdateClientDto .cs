using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Core.DTOs
{
    public class UpdateClientDto : ClientDto
    {
        public Guid Id { get; set; }
    }
}
