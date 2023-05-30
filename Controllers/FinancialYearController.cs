using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Security.Claims;

namespace ScopoERP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class FinancialYearController : ControllerBase
{
    private readonly ILogger<FinancialYearController> _logger;
    private readonly FinancialYearService _FinancialYearService;

    public FinancialYearController(ILogger<FinancialYearController> logger, FinancialYearService FinancialYearService)
    {
        _logger = logger;
        _FinancialYearService = FinancialYearService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinancialYearViewModel>>> Get(int pageIndex = 1, int pageSize = 10, bool excelExport=false)
    {
        return Ok(await _FinancialYearService.Get(pageIndex, pageSize,excelExport));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FinancialYearViewModel>> GetByID(int id)
    {
        return Ok(await _FinancialYearService.GetByID(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(FinancialYearViewModel FinancialYearVM)
    {
        return Ok(await _FinancialYearService.Create(FinancialYearVM));
    }

    [HttpPut]
    public async Task<ActionResult<FinancialYearViewModel>> Put(FinancialYearViewModel FinancialYearVM)
    {
        return Ok(await _FinancialYearService.Update(FinancialYearVM));
    }
    [HttpDelete]
    public async Task<ActionResult<int>> Delete(FinancialYearViewModel FinancialYearVM)
    {
        var currentUserName = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
        return Ok(await _FinancialYearService.Delete(FinancialYearVM.Id, currentUserName));
    }
}

