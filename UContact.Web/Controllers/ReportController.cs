using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UContact.Web.HttpClients;
using UContact.Web.Models.Reports;

namespace UContact.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportHttpClient _reportHttpClient;

        public ReportController(ReportHttpClient reportHttpClient)
        {
            _reportHttpClient = reportHttpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List([FromQuery] int take = 10, [FromQuery] int skip = 0)
        {
            var persons = await _reportHttpClient.GetReports(take, skip);
            return View(persons);
        }
        public async Task<IActionResult> InsertOrUpdate([FromQuery] Guid? Id)
        {
            var model = new ReportViewModel();

            if (Id.HasValue)
                model = await _reportHttpClient.GetReportById(Id.Value);
            else
                model = new ReportViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdate(ReportViewModel model)
        {
            var update = await _reportHttpClient.CreateReport(model);
            return Ok("CREATED");
        }

    }
}
