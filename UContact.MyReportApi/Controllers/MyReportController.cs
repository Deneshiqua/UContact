using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UContact.MyReportApi.Database.Entities;
using UContact.MyReportApi.Database.Services;
using UContact.MyReportApi.Models;

namespace UContact.MyReportApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/v{version:apiVersion}/myreports")]
    public class MyReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        public MyReportController(IReportService reportService,
            IMapper mapper)
        {
            this._reportService = reportService;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int take = 10, int skip = 0)
        {
            var reports = await _reportService.GetAll(take, skip);
            return Ok(reports);
        }

        [HttpGet("{id:Guid?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
                return BadRequest();

            var report = await _reportService.GetById(id.Value);
            if (report == null)
                return NotFound("Report not found.");

            var model = _mapper.Map<Report, ReportModel>(report);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] ReportModel model)
        {
            if (model is null)
                return BadRequest();

            model.Status = ReportStatus.Generating;
            model.CreatedOn = DateTime.Now;

            var entity = _mapper.Map<ReportModel, Report>(model);
            await _reportService.Insert(entity);

            return Ok(entity);
        }

    }
}
