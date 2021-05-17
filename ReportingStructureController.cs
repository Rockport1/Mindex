using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using challenge.Services;
using challenge.resources;
using Microsoft.AspNetCore.Mvc;
using challenge.Models;
using Microsoft.Extensions.Logging;

namespace challenge.Controllers
{
    [Route("api/reportingstructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController (ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService, IMapper mapper)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            var reportingStructure = _reportingStructureService.GetById(id);
            var resource = _mapper.Map<ReportingStructure, ReportingStructureResource>(reportingStructure);

            if (reportingStructure == null)
                return NotFound();

            return Ok(resource);
            //return Ok(reportingStructure);
        }


        /*
        [HttpGet]
        public async Task<IEnumerable<ReportingStructureResource>> GetAllAsync()
        {
            var reporintgStructure = await _reportingStructureService.ListAsync();
            var resources = _mapper.Map<IEnumerable<ReportingStructure>,  IEnumerable<ReportingStructureResource>>(reporintgStructure);
            return resources;
        }
        */
    }
}
