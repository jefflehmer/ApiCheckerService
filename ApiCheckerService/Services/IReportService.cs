using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCheckerService.Services
{
    public interface IReportService
    {
        Task<string> Generate(string ipdomain, string source);
    }
}
