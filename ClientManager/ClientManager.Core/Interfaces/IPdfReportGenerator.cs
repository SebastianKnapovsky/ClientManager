using ClientManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Core.Interfaces
{
    public interface IPdfReportGenerator
    {
        byte[] Generate(IEnumerable<Client> clients);
    }
}
