using ClientManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("download")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Generating client report PDF");

            var clients = await _clientService.GetAllClientsForReportAsync();
            var pdfBytes = _pdfGenerator.Generate(clients);

            return File(pdfBytes, "application/pdf", "ClientReport.pdf");
        }
    }
}
