using DinkToPdf;
using DinkToPdf.Contracts;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using System;

namespace ReportPOC.Services
{
    public class PdfGenerator
    {
        private readonly IConverter _converter;

        public PdfGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratorPdf(string htmlContent)
        {
            // Set global settings for PDF
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Right = 10, Bottom = 10, Left = 10 }
            };

            // Set object settings for PDF
            var objectSettings = new ObjectSettings
            {
                HtmlContent = htmlContent,
                WebSettings =
                {
                    DefaultEncoding = "utf-8",
                    LoadImages = true, // Load images
                    EnableIntelligentShrinking = true // Shrink content to fit the page
                }
            };

           
            // Create PDF document
            var pdfDocument = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            // Convert HTML to PDF
            byte[] pdf = _converter.Convert(pdfDocument);

            return pdf;
        }



       
    }
}
