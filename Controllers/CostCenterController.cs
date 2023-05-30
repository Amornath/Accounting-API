using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Security.Claims;

namespace ScopoERP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class CostCenterController : ControllerBase
{
    private readonly ILogger<CostCenterController> _logger;
    private readonly CostCenterService _CostCenterService;

    public CostCenterController(ILogger<CostCenterController> logger, CostCenterService CostCenterService)
    {
        _logger = logger;
        _CostCenterService = CostCenterService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostCenterViewModel>>> Get(int pageIndex = 1, int pageSize = 10)
    {
        return Ok(await _CostCenterService.Get(pageIndex, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostCenterViewModel>> GetByID(int id)
    {
        return Ok(await _CostCenterService.GetByID(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(CostCenterViewModel CostCenterVM)
    {
        return Ok(await _CostCenterService.Create(CostCenterVM));
    }

    [HttpPut]
    public async Task<ActionResult<CostCenterViewModel>> Put(CostCenterViewModel CostCenterVM)
    {
        return Ok(await _CostCenterService.Update(CostCenterVM));
    }
    [HttpDelete]
    public async Task<ActionResult<int>> Delete(CostCenterViewModel CostCenterVM)
    {
        var currentUserName = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
        return Ok(await _CostCenterService.Delete(CostCenterVM.Id, currentUserName));
    }
}

