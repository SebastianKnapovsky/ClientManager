using ClientManager.Core.Interfaces;
using ClientManager.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ClientManager.Infrastructure.Services
{
    public class PdfReportGenerator : IPdfReportGenerator
    {
        public byte[] Generate(IEnumerable<Client> clients)
        {
            var clientList = clients?.ToList() ?? new List<Client>();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Text("Client Report")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Name").Bold();
                            header.Cell().Element(CellStyle).Text("Address").Bold();
                            header.Cell().Element(CellStyle).Text("NIP").Bold();
                            header.Cell().Element(CellStyle).Text("Additional Fields").Bold();
                        });

                        foreach (var client in clientList)
                        {
                            table.Cell().Element(CellStyle).Text(client.Name ?? string.Empty);
                            table.Cell().Element(CellStyle).Text(client.Address ?? string.Empty);
                            table.Cell().Element(CellStyle).Text(client.NIP ?? string.Empty);

                            string additionalText = "-";
                            if (client.AdditionalFields != null && client.AdditionalFields.Any())
                            {
                                additionalText = string.Join("\n", client.AdditionalFields.Select(f =>
                                {
                                    var name = f?.NameField ?? string.Empty;
                                    var value = f?.Value ?? string.Empty;
                                    return $"{name}: {value}";
                                }));
                            }

                            table.Cell().Element(CellStyle).Text(additionalText);
                        }

                        static IContainer CellStyle(IContainer container) =>
                            container
                                .Padding(5)
                                .BorderBottom(1)
                                .BorderColor(Colors.Grey.Lighten2);
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
