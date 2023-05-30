using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Security.Claims;

namespace ScopoERP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class SubsidiaryAccountController : ControllerBase
{
    private readonly ILogger<SubsidiaryAccountController> _logger;
    private readonly SubsidiaryAccountService _SubsidiaryAccountService;

    public SubsidiaryAccountController(ILogger<SubsidiaryAccountController> logger, SubsidiaryAccountService SubsidiaryAccountService)
    {
        _logger = logger;
        _SubsidiaryAccountService = SubsidiaryAccountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubsidiaryAccountViewModel>>> Get(int pageIndex = 1, int pageSize = 10, bool excelExport = false, int? accountTypeId = null, int? parentAccountId = null, int? accountId = null)
    {
        return Ok(await _SubsidiaryAccountService.Get(pageIndex, pageSize,excelExport, accountTypeId, parentAccountId, accountId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubsidiaryAccountViewModel>> GetByID(int id)
    {
        return Ok(await _SubsidiaryAccountService.GetByID(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(SubsidiaryAccountViewModel SubsidiaryAccountVM)
    {
        return Ok(await _SubsidiaryAccountService.Create(SubsidiaryAccountVM));
    }

    [HttpPut]
    public async Task<ActionResult<SubsidiaryAccountViewModel>> Put(SubsidiaryAccountViewModel SubsidiaryAccountVM)
    {
        return Ok(await _SubsidiaryAccountService.Update(SubsidiaryAccountVM));
    }
    [HttpDelete]
    public async Task<ActionResult<int>> Delete(SubsidiaryAccountViewModel SubsidiaryAccountVM)
    {
        var currentUserName = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
        return Ok(await _SubsidiaryAccountService.Delete(SubsidiaryAccountVM.Id, currentUserName));
    }
}

