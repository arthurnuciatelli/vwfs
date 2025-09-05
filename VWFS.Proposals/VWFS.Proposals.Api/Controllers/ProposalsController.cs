using Microsoft.AspNetCore.Mvc;
using VWFS.Proposals.Application.DTOs;
using VWFS.Proposals.Application.Interfaces;
using VWFS.Proposals.Application.Services;

namespace VWFS.Proposals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProposalsController : ControllerBase
{
    private readonly IProposalAppService _appService;

    public ProposalsController(IProposalAppService appService)
    {
        _appService = appService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProposalRequestDto dto)
    {
        var response = await _appService.CreateProposalAsync(dto);
        if (response.Status == "Error") return BadRequest(response);
        return Ok(response);
    }
}
