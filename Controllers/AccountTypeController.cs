using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Security.Claims;

namespace ScopoERP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AccountTypeController : ControllerBase
{
    private readonly ILogger<AccountTypeController> _logger;
    private readonly AccountTypeService _AccountTypeService;

    public AccountTypeController(ILogger<AccountTypeController> logger, AccountTypeService AccountTypeService)
    {
        _logger = logger;
        _AccountTypeService = AccountTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountTypeViewModel>>> Get(int pageIndex = 1, int pageSize = 10)
    {
        var companyid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(await _AccountTypeService.Get(pageIndex, pageSize, Convert.ToInt32(companyid)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountTypeViewModel>> GetByID(int id)
    {
        var companyid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(await _AccountTypeService.GetByID(id, Convert.ToInt32(companyid)));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(AccountTypeViewModel AccountTypeVM)
    {
        var companyid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        AccountTypeVM.CompanyId = Convert.ToInt32(companyid);
        if (!_AccountTypeService.IsUnique(AccountTypeVM))
        {
            return Ok(await _AccountTypeService.Create(AccountTypeVM));
        }
        return BadRequest("Account Type Already Exist !");
       
    }

    [HttpPut]
    public async Task<ActionResult<AccountTypeViewModel>> Put(AccountTypeViewModel AccountTypeVM)
    {
        var companyid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        AccountTypeVM.CompanyId = Convert.ToInt32(companyid);
        if (!_AccountTypeService.IsUnique(AccountTypeVM))
        {
            return Ok(await _AccountTypeService.Update(AccountTypeVM));
        }
        return BadRequest("Account Type Already Exist !");
    }
    [HttpDelete]
    public async Task<ActionResult<int>> Delete(AccountTypeViewModel AccountTypeVM)
    {
        var currentUserName = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
        return Ok(await _AccountTypeService.Delete(AccountTypeVM.Id, currentUserName));
    }
}
