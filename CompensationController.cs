using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using challenge.Services;
using challenge.Models;
using challenge.resources;
using AutoMapper;
using Microsoft.Extensions.Logging;
using challenge.Extensions;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly IMapper _mapper;

        public CompensationController(ILogger<ReportingStructureController> logger, ICompensationService compensationService, IMapper mapper)
        {
            _logger = logger;
            _compensationService = compensationService;
            _mapper = mapper;
        }

        [HttpPost]
        //public IActionResult CreateCompensation([FromBody] Compensation compensation)
        //public IActionResult CreateCompensation([FromBody] Compensation compensation)
        public IActionResult CreateCompensation([FromBody] SaveCompensationResource compensationResource)
        {
            //_logger.LogDebug($"Received compensation create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var compensation = _mapper.Map<SaveCompensationResource, Compensation>(compensationResource);
            var result = _compensationService.Create(compensation);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var compResource = _mapper.Map<Compensation, CompensationResource>(result.Compensation);
            return Ok(compResource);
            //return CreatedAtRoute("getEmployeeById", new { id = compensation.EmployeeId }, compensation);
            //return CreatedAtRoute("getEmployeeById", new { id = compensation.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

    }
}
