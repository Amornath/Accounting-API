using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Security.Claims;
using ScopoERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ScopoERP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly TransactionService _TransactionService;
    private ERPContext _db;

    public TransactionController(ILogger<TransactionController> logger, TransactionService TransactionService, ERPContext db)
    {
        _logger = logger;
        _TransactionService = TransactionService;
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionViewModel>>> Get(int pageIndex = 1, int pageSize = 10, bool excelExport=false, int? transactionId = null, string voucherTypeName = null, DateTime? fromDate = null, DateTime? toDate =null)
    {
        return Ok(await _TransactionService.Get(pageIndex, pageSize,excelExport, transactionId, voucherTypeName, fromDate, toDate));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionViewModel>> GetByID(int id)
    {
        return Ok(await _TransactionService.GetByID(id));
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post(TransactionViewModel TransactionVM)
    {
        return Ok(await _TransactionService.Create(TransactionVM));
    }




    [HttpPut]
    public async Task<ActionResult<TransactionViewModel>> Put(TransactionViewModel TransactionVM)
    {
        return Ok(await _TransactionService.Update(TransactionVM));
    }
    [HttpDelete]
    public async Task<ActionResult<int>> Delete(TransactionViewModel TransactionVM)
    {
        var currentUserName = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
        return Ok(await _TransactionService.Delete(TransactionVM.Id, currentUserName));
    }
}
