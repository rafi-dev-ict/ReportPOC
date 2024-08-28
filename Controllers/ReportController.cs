using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportPOC.Services;
using System.Net.Mime;

namespace ReportPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{reportName}/{reportType}")]
        public ActionResult Get(string reportName, string reportType)
        {
            var reportNameWithLang = reportName;
            var reportFileByteString = _reportService.GenerateReportAsync(reportNameWithLang, reportType);
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportNameWithLang, reportType));
        }


        private string getReportName(string reportName, string reportType)
        {
            var outputFileName = reportName + ".pdf";

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    outputFileName = reportName + ".pdf";
                    break;
                case "XLS":
                    outputFileName = reportName + ".xls";
                    break;
                case "WORD":
                    outputFileName = reportName + ".doc";
                    break;
            }

            return outputFileName;
        }

    
}
}
