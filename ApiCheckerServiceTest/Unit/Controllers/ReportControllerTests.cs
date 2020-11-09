using System;
using System.Threading;
using System.Threading.Tasks;
using ApiCheckerService.Controllers;
using ApiCheckerService.Queries;
using ApiCheckerService.Services;
using ApiCheckerServiceTest.Unit.Queries;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ApiCheckerServiceTest.Unit.Controllers
{
    public class ReportControllerTests
    {
        // TODO: these are "unit" tests for the controller but a good e2e test would call the endpoint through it's http endpoint
        [Theory]
        [InlineData("www.cnn.com")]
        public async void Given_SimpleUrl_When_NoSources_Then_ReturnsSimpleReport(string url)
        {
            // Arrange
            var generateReportResult = new GenerateReport.Result
            {
                Report = url,
            };

            var generateReport = A.Fake<GenerateReport>();

            var token = new CancellationToken(false);
            A.CallTo(() => generateReport.Execute(A<IReportService>.Ignored))
                .Returns(Task.FromResult(generateReportResult));

            var reportService = new ReportService();
            var logger = A.Fake<ILogger<ReportController>>();
            var sut = new ReportController(reportService, logger);

            // Act
            var result = await sut.Generate(url, token);

            // Assert
            var actionResult = Assert.IsType<ActionResult<GenerateReport.Result>>(result);
            //var returnValue = Assert.IsType<GenerateReport.Result>(actionResult.Value);

            Assert.NotNull(actionResult.Value?.Report);
            Assert.Equal(generateReportResult, actionResult.Value);

            A.CallTo(() => generateReport.Execute(A<IReportService>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData("http://www.cnn.com")]
        public void Given_UnknownInput_When_CouldBeIpOrDomain_Then_HandlesSuccessfully(string url)
        {

        }

        [Theory]
        [InlineData("http://www.cnn.com")]
        public void Given_InvalidInput_When_Unparseable_Then_ReturnsStatus400(string url)
        {

        }
    }
}
