using ClientManager.Core.Interfaces;
using ClientManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IPdfReportGenerator _pdfGenerator;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IClientService clientService, IPdfReportGenerator pdfGenerator, ILogger<ReportController> logger)
        {
            _clientService = clientService;
            _pdfGenerator = pdfGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Generating client report PDF");

            var clients = await _clientService.GetAllClientsAsync();
            var pdfBytes = _pdfGenerator.Generate(clients);

            return File(pdfBytes, "application/pdf", "ClientReport.pdf");
        }
    }
}
