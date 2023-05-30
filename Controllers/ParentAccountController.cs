using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Security.Claims;

namespace ScopoERP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class ParentAccountController : ControllerBase
{
    private readonly ILogger<ParentAccountController> _logger;
    private readonly ParentAccountService _ParentAccountService;

    public ParentAccountController(ILogger<ParentAccountController> logger, ParentAccountService ParentAccountService)
    {
        _logger = logger;
        _ParentAccountService = ParentAccountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParentAccountViewModel>>> Get(int pageIndex = 1, int pageSize = 10, bool excelExport=false, int? accountTypeId = null, int? parentaccountId = null)
    {
        return Ok(await _ParentAccountService.Get(pageIndex, pageSize,excelExport, accountTypeId, parentaccountId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParentAccountViewModel>> GetByID(int id)
    {
        return Ok(await _ParentAccountService.GetByID(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(ParentAccountViewModel ParentAccountVM)
    {
        return Ok(await _ParentAccountService.Create(ParentAccountVM));
    }

    [HttpPut]
    public async Task<ActionResult<ParentAccountViewModel>> Put(ParentAccountViewModel ParentAccountVM)
    {
        return Ok(await _ParentAccountService.Update(ParentAccountVM));
    }
    [HttpDelete]
    public async Task<ActionResult<int>> Delete(ParentAccountViewModel ParentAccountVM)
    {
        var currentUserName = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
        return Ok(await _ParentAccountService.Delete(ParentAccountVM.Id, currentUserName));
    }
}

