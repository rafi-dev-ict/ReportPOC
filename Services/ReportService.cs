using AspNetCore.Reporting;
using ReportPOC.Models;
using System.Reflection;
using System.Text;

namespace ReportPOC.Services
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName, string reportType);
    }

    public class ReportService : IReportService
    {
        public byte[] GenerateReportAsync(string reportName, string reportType)
        {
            // Get the directory of the executing assembly (your application root)
            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Construct the full path to the RDLC file
            string rdlcFilePath = Path.Combine(rootPath, "ReportFiles", $"{reportName}.rdlc");

            if (!File.Exists(rdlcFilePath))
            {
                throw new FileNotFoundException($"The report file was not found at path: {rdlcFilePath}");
            }

            // Register the necessary encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            LocalReport report = new LocalReport(rdlcFilePath);

            // Prepare data for the report
            List<UserDto> userList = new List<UserDto>
            {
                new UserDto { FirstName = "jp", LastName = "jan", Email = "jp@gm.com", Phone = "+976666661111" },
                new UserDto { FirstName = "jp2", LastName = "jan", Email = "jp2@gm.com", Phone = "+976666661111" },
                new UserDto { FirstName = "முதல் பெயர்", LastName = "கடைசி பெயர்", Email = "jp3@gm.com", Phone = "+976666661111" },
                new UserDto { FirstName = "पहला नाम", LastName = "अंतिम नाम", Email = "jp4@gm.com", Phone = "+976666661111" },
                new UserDto { FirstName = "jp5", LastName = "jan", Email = "jp5@gm.com", Phone = "+976666661111" }
            };

            //report.AddDataSource("dsUsers", userList);

            // Create a dictionary for parameters and add the required 'title' parameter
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "title", "Hello world" }
            };

            var result = report.Execute(GetRenderType(reportType), 1, parameters);
           

            return result.MainStream;
        }

        private RenderType GetRenderType(string reportType)
        {
            return reportType.ToUpper() switch
            {
                "XLS" => RenderType.Excel,
                "WORD" => RenderType.Word,
                _ => RenderType.Pdf
            };
        }
    }
}
