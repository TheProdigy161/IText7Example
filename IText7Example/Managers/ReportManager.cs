using ITest7Example.Interfaces;
using ITest7Example.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ITest7Example.Managers
{
    public class ReportManager : IReportManager
    {
        public ReportManager()
        {

        }

        public async Task<byte[]> GenerateReportData(List<Test> tests)
        {
            //https://itextpdf.com/en/resources/books/itext-7-jump-start-tutorial-net/chapter-1

            //Setup fonts for report.
            PdfFont headerFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont tableFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            //Setup PDF document readers and writers.
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            //Create the report logo.
            Image logo = await CreateLogo("images/logo-header.png");
            document.Add(logo);

            //TODO - Information block.

            //Create the report table.
            Table table = await CreateTable(tests, headerFont, tableFont);
            document.Add(table);
            document.Close();

            //TODO - Generic Page Footer

            return stream.ToArray();
        }

        /// <summary>
        /// Create the logo for the report.
        /// </summary>
        /// <param name="path">The path of the image that will be used as the logo.</param>
        /// <returns>An image to be added to the PDF document.</returns>
        private async Task<Image> CreateLogo(string path)
        {
            return await Task.Run(() =>
            {
                Image logo = new Image(ImageDataFactory.Create(path));
                logo.SetHeight(UnitValue.CreatePercentValue(25));
                logo.SetWidth(UnitValue.CreatePercentValue(25));
                //Adds spacing between the logo and the table.
                logo.SetMarginBottom(25);

                //logo.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

                return logo;
            });
        }

        /// <summary>
        /// Creates the table that will store the required test data.
        /// </summary>
        /// <param name="tests">The test data that will be used to create the table.</param>
        /// <param name="headerFont">The font for the header columns.</param>
        /// <param name="tableFont">The font for the rows.</param>
        /// <returns>A table that will be added to the PDF document.</returns>
        private async Task<Table> CreateTable(List<Test> tests, PdfFont headerFont, PdfFont tableFont)
        {
            return await Task.Run(() =>
            {
                Table table = new Table(new float[] { 1, 1, 1 });
                table.SetWidth(UnitValue.CreatePercentValue(100));

                //Create header columns.
                table.AddHeaderCell(new Cell().Add(new Paragraph("ID").SetFont(headerFont)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("TestName").SetFont(headerFont)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Result").SetFont(headerFont)));

                //Create each row.
                foreach(Test test in tests)
                {
                    table.AddCell(new Cell().Add(new Paragraph(test.ID.ToString()).SetFont(tableFont)));
                    table.AddCell(new Cell().Add(new Paragraph(test.TestName).SetFont(tableFont)));
                    table.AddCell(new Cell().Add(new Paragraph(test.Result).SetFont(tableFont)));
                }

                return table;
            });
        }
    }
}
