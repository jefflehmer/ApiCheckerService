using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiCheckerService.Queries;
using ApiCheckerService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiCheckerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        private readonly ILogger<ReportController> logger;

        public ReportController(IReportService service, ILogger<ReportController> logger)
        {
            this.reportService = service;
            this.logger = logger;
        }

        [HttpGet("generate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenerateReport.Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenerateReport.Result>> Generate(string ipdomain, CancellationToken cancellationToken)//, [FromBody] List<string> sources = null)
        {
            // TODO: attach fluentvalidation to mediatr to automatically validate at command
            if (!GenerateReport.Validate(ipdomain, null))// sources))
                return this.BadRequest();

            try
            {
                var command = new GenerateReport(ipdomain, null);// sources);
                var report = await command.Execute(this.reportService);
                return report;
            }
            catch (Exception ex)
            {
                // TODO: too generic! handle exception better
                this.logger.LogCritical(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("test")]
        public string Test()
        {
            return "This is a test of the Emergency Broadcasting System!";
        }
    }
}
