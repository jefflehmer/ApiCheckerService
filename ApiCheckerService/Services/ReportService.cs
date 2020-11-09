using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiCheckerService.Services
{
    public class ReportService : IReportService
    {
        public async Task<string> Generate(string ipdomain, string source)
        {
            try
            {
                // TODO: replace with actual calls to endpoint
                var ip = await GetIpFromDomain(ipdomain);
                return ip.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected async Task<IPAddress> GetIpFromDomain(string ipdomain)
        {
            // NOTE: the command should have already verified this is an IP or a Domain
            if (Uri.TryCreate(ipdomain, UriKind.Absolute, out var uri))
            {
                var addresses =  await Dns.GetHostAddressesAsync(ipdomain);
                return addresses[0];
            }
            else
            {
                return IPAddress.Parse(ipdomain);
            }

        }
    }
}
