using System.Collections.Generic;
using System.Threading.Tasks;
using ITest7Example.Interfaces;
using ITest7Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITest7Example.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportManager _ReportManager;

        public ReportController(ILogger<ReportController> logger, IReportManager reportManager)
        {
            _logger = logger;
            _ReportManager = reportManager;
        }

        /// <summary>
        /// Generates the report and downloads the file on the web application.
        /// </summary>
        /// <returns>A file that will be downloaded on the web application.</returns>
        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            List<Test> tests = new List<Test>()
            {
                new Test()
                {
                    ID = 1,
                    TestName = "Test 1",
                    Result = "Positive"
                },
                new Test()
                {
                    ID = 2,
                    TestName = "Test 2",
                    Result = "Negative"
                },
                new Test()
                {
                    ID = 3,
                    TestName = "Test 3",
                    Result = "Positive"
                },
                new Test()
                {
                    ID = 4,
                    TestName = "Test 4",
                    Result = "Positive"
                },
                new Test()
                {
                    ID = 5,
                    TestName = "Test 5",
                    Result = "Negative"
                }
            };

            byte[] reportData = await _ReportManager.GenerateReportData(tests);

            return File(reportData, "application/pdf", "TestPDF.pdf");
        }
    }
}
