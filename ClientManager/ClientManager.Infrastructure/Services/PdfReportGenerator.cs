using ClientManager.Core.Interfaces;
using ClientManager.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;

namespace ClientManager.Infrastructure.Services
{
    public class PdfReportGenerator : IPdfReportGenerator
    {
        public byte[] Generate(IEnumerable<Client> clients)
        {
            var clientList = clients.ToList(); 

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
                            table.Cell().Element(CellStyle).Text(client.Name);
                            table.Cell().Element(CellStyle).Text(client.Address);
                            table.Cell().Element(CellStyle).Text(client.NIP);

                            string additional = client.AdditionalFields != null && client.AdditionalFields.Count > 0
                                ? string.Join("\n", client.AdditionalFields.Select(kv => $"{kv.Key}: {kv.Value}"))
                                : "-";

                            table.Cell().Element(CellStyle).Text(additional);
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
