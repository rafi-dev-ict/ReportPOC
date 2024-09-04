using SelectPdf;

namespace ReportPOC.Services
{
    public interface IPdfService
    {
        byte[] GeneratePdfFromHtml(string htmlContent);
    }

    public class PdfService : IPdfService
    {
        public byte[] GeneratePdfFromHtml(string htmlContent)
        {
            HtmlToPdf converter = new HtmlToPdf();

            // Set converter options if needed
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;
            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;

            PdfDocument document = converter.ConvertHtmlString(htmlContent);
            byte[] pdfBytes = document.Save();
            document.Close();

            return pdfBytes;
        }
    }
}
