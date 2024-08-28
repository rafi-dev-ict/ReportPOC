﻿using AspNetCore.Reporting;
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
            //string rdlcFilePath = Path.Combine(rootPath, "ReportFiles", $"{reportName}.rdlc");

            //string rdlcFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportFiles", "Report1.rdlc");
            //string rdlcFilePath = @"F:\POC\ReportPOC\ReportFiles\Report1.rdlc";

            string rdlcFilePath = "";
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var ReportFilePath = root.GetSection("ReportFilePath").GetChildren();

            foreach (var path in ReportFilePath.ToList())
            {
                if (path.Key == "rdlc") rdlcFilePath = path.Value + reportName + ".rdlc";
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");


            if (!File.Exists(rdlcFilePath))
            {
                throw new FileNotFoundException($"The report file was not found at path: {rdlcFilePath}");
            }

            // Register the necessary encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            LocalReport report = new LocalReport(rdlcFilePath);

           

            //report.AddDataSource("dsUsers", userList);

            // Create a dictionary for parameters and add the required 'title' parameter
            var report_parameters = new Dictionary<string, string> { { "CompileVersion", "v.4.7" } };
            report_parameters.Add("title", "BURO Bangladesh");

            var result = report.Execute(GetRenderType(reportType), 1, report_parameters);
           

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
