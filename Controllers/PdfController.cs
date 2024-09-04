using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportPOC.Services;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using ReportPOC.Models;
using System.Text.Json;
using System.Reflection;
using ReportPOC.ReportFiles;

namespace ReportPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfGenerator _pdfGenerator;
        private readonly IPdfService _pdfService;
        private readonly TestHtmlPdf _testHtmlPdf;

        public PdfController(PdfGenerator pdfGenerator, IPdfService pdfService, TestHtmlPdf testHtmlPdf)
        {
            this._pdfGenerator = pdfGenerator;
            _pdfService = pdfService; _testHtmlPdf = testHtmlPdf;
        }

        [HttpPost("drinkTopdf")]
        public async Task<IActionResult> GenerateDrinkPdf()
        {
            string title = "title";
            string content = "lorem";

            // Generate HTML content dynamically using model data
            string htmlContent = $@"
                <html>
                    <head>
                        <meta charset='utf-8'>
                        <style>
                            body {{ font-family: Arial, sans-serif; }}
                            h1 {{ color: #333; }}
                        </style>
                    </head>
                    <body>
                        <h1>{title}</h1>
                        <img src='https://images.unsplash.com/photo-1484788984921-03950022c9ef?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bGFwdG9wfGVufDB8fDB8fHww' alt='Example Image' />
                        <p>{content}</p>
                        <p>Date: {DateTime.Now}</p>
                    </body>
                </html>";




            byte[] pdfBytes = _pdfGenerator.GeneratorPdf(htmlContent);

            return File(pdfBytes, "application/pdf", "generated.pdf");




        }


        public class PdfRequest
        {
            public string HtmlContent { get; set; }
        }
        [HttpPost("selectpdf")]
        public IActionResult GeneratePdf()
        {
            //if (string.IsNullOrWhiteSpace(request.HtmlContent))
            //{
            //    return BadRequest("HTML content cannot be empty.");
            //}

            var model = new PdfModel
            {
                Title = "Dynamic HTML Document",
                Name = "John Doe",
                Age = 30,
                City = "New York",
                Image= "https://images.unsplash.com/photo-1484788984921-03950022c9ef?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bGFwdG9wfGVufDB8fDB8fHww",
                Users = new List<UserInfo>
                {
                    new UserInfo { Id = 1, Name = "Alice", Age = 25, City = "Los Angeles" },
                    new UserInfo { Id = 2, Name = "Bob", Age = 28, City = "Chicago" },
                    new UserInfo { Id = 3, Name = "Charlie", Age = 35, City = "San Francisco" }
                }
            };

            var htmlContent = _testHtmlPdf.GenerateSamplePdfTemplate(model);

            byte[] pdfBytes = _pdfService.GeneratePdfFromHtml(htmlContent);
            return File(pdfBytes, "application/pdf", "GeneratedDocument.pdf");
        }

    }

}
