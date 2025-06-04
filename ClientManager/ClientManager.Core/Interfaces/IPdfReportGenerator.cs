using ClientManager.Core.Models;

namespace ClientManager.Core.Interfaces
{
    public interface IPdfReportGenerator
    {
        byte[] Generate(IEnumerable<Client> clients);
    }
}
