using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using ApiCheckerService.Services;

namespace ApiCheckerService.Queries
{
    public class GenerateReport
    {
        protected string ipdomain { get; set; }
        protected List<string> sources { get; set; }

        protected List<string> defaultSources = new List<string>
        {
            "cnn.com",
            "bbc.co.uk",
            "stackoverflow.com"
        };

        public GenerateReport(string ipdomain, List<string> sources)
        {
            this.ipdomain = ipdomain;
            this.sources = sources ?? defaultSources;
        }

        public class Result
        {
            public string Report { get; set; }
        }

        public static bool Validate(string ipdomain, List<string> sources)
        {
            return Uri.TryCreate(ipdomain, UriKind.Absolute, out var uri);

            // TODO: validate the input "sources"
        }

        // TODO: loop through each of the input sources and build the report
        public virtual async Task<Result> Execute(IReportService reportService)
        {
            var tasks = new List<Task<string>>();
            foreach (var source in this.sources)
            {
                tasks.Add(Task.Run(() => reportService.Generate(ipdomain, source)));
            }

            Task.WaitAll(tasks.ToArray());

            var report = string.Empty;
            foreach (var task in tasks)
            {
                // TODO: this should return better than a "raw" string concatenation as the combined reports
                report += task.Result + "\r\n";
            }

            return new Result
            {
                Report = report
            };
        }
    }
}
