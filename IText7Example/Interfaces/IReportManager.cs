using ITest7Example.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITest7Example.Interfaces
{
    public interface IReportManager
    {
        /// <summary>
        /// Generate the report data bytes that will be used to create a pdf for download.
        /// </summary>
        /// <param name="tests">The test data that will be shown on the report table.</param>
        /// <returns>Returns the bytes array for the PDF report.</returns>
        Task<byte[]> GenerateReportData(List<Test> tests);
    }
}
