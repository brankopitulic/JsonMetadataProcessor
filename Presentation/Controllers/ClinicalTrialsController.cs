using Application.Commands.Process;
using Application.Queries.Get;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;
using Presentation.Requests;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicalTrialsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClinicalTrialsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload")]
    [ServiceFilter(typeof(ValidateFileSizeAttribute))] // 1 MB size limit
    public async Task<IActionResult> UploadJsonFile([FromForm] UploadJsonFileRequest request)
    {
        var id = await _mediator.Send(new ProcessClinicalTrialCommand { File = request.File });
        return CreatedAtAction(nameof(GetClinicalTrialById), new { id }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClinicalTrialById(int id)
    {
        var result = await _mediator.Send(new GetClinicalTrialByIdQuery { Id = id });
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetClinicalTrials([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] TrialStatus status = default)
    {
        var result = await _mediator.Send(new GetClinicalTrialsQuery
        {
            Skip = skip,
            Take = take,
            Status = status
        });

        return Ok(result);
    }
}
