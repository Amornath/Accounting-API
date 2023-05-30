using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Security.Claims;
using ScopoERP.Models;

namespace ScopoERP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly ReportService _ReportService;

    public ReportController(ILogger<ReportController> logger, ReportService ReportService)
    {
        _logger = logger;
        _ReportService = ReportService;
    }

    [HttpGet("TrialBalanceReport")]
    public async Task<ActionResult<IEnumerable<TrialBalanceViewModel>>> TrialBalanceReport(DateTime? toDate = null)
    {
        return Ok(await _ReportService.TrialBalance(toDate));
    }

    [HttpGet("BalanceSheetReport")]
    public async Task<ActionResult<IEnumerable<TrialBalanceViewModel>>> BalanceSheetReport( DateTime? toDate = null)
    {
        return Ok(await _ReportService.BalanceSheet(toDate));
    }

    [HttpGet("ProfitLossStatementReport")]
    public async Task<ActionResult<IEnumerable<TrialBalanceViewModel>>> ProfitLossStatementReport(DateTime? fromDate = null, DateTime? toDate = null)
    {
        return Ok(await _ReportService.ProfitLossStatement(fromDate, toDate));
    }


    [HttpGet("LedgerReport")]
    public async Task<ActionResult<IEnumerable<LedgerReportViewModel>>> LedgerReport(int accountId, DateTime fromDate, DateTime toDate)
    {
        return Ok(await _ReportService.LedgerReport(accountId, fromDate, toDate));
    }

}

