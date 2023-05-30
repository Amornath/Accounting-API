using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;
using System.ComponentModel.Design;
using Microsoft.Identity.Client;

namespace ScopoERP.Services;

public class ReportService
{
    private ERPContext _db;
    private IMapper _mapper;
    public ReportService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrialBalanceViewModel>> TrialBalance(DateTime? toDate)
    {
        var financialYear = await _db.FinancialYears.OrderBy(x => x.YearStartDate).LastOrDefaultAsync();

        if(financialYear == null)
        {
            financialYear = await _db.FinancialYears.Where(x => x.IsActive == true).OrderBy(x => x.YearStartDate).LastAsync();
        }
        DateTime? fromDate = financialYear.YearStartDate;
        var trialBalance = new List<TrialBalanceViewModel>();

        var accTypes = await _db.AccountTypes.Where(x => x.IsActive == true).ToListAsync();

        foreach (var accType in accTypes)
        {
            var accountGroups = await (from pa in _db.ParentAccounts
                                       where pa.AccountTypeId == accType.Id
                                       && pa.IsActive == true 
                                       && pa.ParentId == null
                                       select new TrialBalanceItem
                                       {
                                           Id = pa.Id,
                                           AccountNo = pa.AccountNo,
                                           AccountName = pa.AccountName,
                                           IsGroup = true
                                       }).ToListAsync();

            await GetChildrens(fromDate, toDate, accountGroups);

            trialBalance.Add(new TrialBalanceViewModel
            {
                AccountType = accType.Name,
                Childrens = accountGroups
            });
        }

        return trialBalance;
    }

    public async Task<IEnumerable<BalanceSheetViewModel>> BalanceSheet( DateTime? toDate)
    {
        var financialYear = await _db.FinancialYears.OrderBy(x => x.YearStartDate).LastOrDefaultAsync();

        if (financialYear == null)
        {
            financialYear = await _db.FinancialYears.Where(x => x.IsActive == true).OrderBy(x => x.YearStartDate).LastAsync();
        }
        DateTime? fromDate = financialYear.YearStartDate;
        var balanceSheet = new List<BalanceSheetViewModel>();

        var accTypes = await _db.AccountTypes.Where(x => x.IsActive == true && (x.Name== "Assets" || x.Name == "Liabilities")).ToListAsync();

        foreach (var accType in accTypes)
        {
            var accountGroups = await (from pa in _db.ParentAccounts
                                       where pa.AccountTypeId == accType.Id
                                       && pa.IsActive == true 
                                       && pa.ParentId == null
                                       select new TrialBalanceItem
                                       {
                                           Id = pa.Id,
                                           AccountNo = pa.AccountNo,
                                           AccountName = pa.AccountName,
                                           IsGroup = true
                                       }).ToListAsync();

            await GetChildrens(fromDate, toDate, accountGroups);

            balanceSheet.Add(new BalanceSheetViewModel
            {
                AccountType = accType.Name,
                Childrens = accountGroups
            });
        }

        return balanceSheet;

    }

    public async Task<IEnumerable<BalanceSheetViewModel>> ProfitLossStatement(DateTime? fromDate, DateTime? toDate)
    {
        var profitLoss = new List<BalanceSheetViewModel>();

        var accTypes = await _db.AccountTypes.Where(x => x.IsActive == true && (x.Name == "Income" || x.Name == "Expense")).ToListAsync();

        foreach (var accType in accTypes)
        {
            var accountGroups = await (from pa in _db.ParentAccounts
                                       where pa.AccountTypeId == accType.Id
                                       && pa.IsActive == true 
                                       && pa.ParentId == null
                                       select new TrialBalanceItem
                                       {
                                           Id = pa.Id,
                                           AccountNo = pa.AccountNo,
                                           AccountName = pa.AccountName,
                                           IsGroup = true
                                       }).ToListAsync();

            await GetChildrens(fromDate, toDate, accountGroups);

            profitLoss.Add(new BalanceSheetViewModel
            {
                AccountType = accType.Name,
                Childrens = accountGroups
            });
        }

        return profitLoss;
    }

    private async Task<bool> GetChildrens( DateTime? fromDate, DateTime? toDate, List<TrialBalanceItem> accountGroups)
    {
        

        if (accountGroups != null && accountGroups.Count > 0)
        {
            foreach (var parent in accountGroups)
            {

                var _accGroups = await (from pa in _db.ParentAccounts
                                        where pa.ParentId == parent.Id
                                        && pa.IsActive == true 
                                        select new TrialBalanceItem
                                        {
                                            Id = pa.Id,
                                            AccountNo = pa.AccountNo,
                                            AccountName = pa.AccountName,
                                            IsGroup = true
                                        }).ToListAsync();

                var _accounts = await (from t in _db.Transactions
                                       join td in _db.TransactionDetails on t.Id equals td.TransactionId
                                       join sa in _db.SubsidiaryAccounts on td.AccountId equals sa.Id
                                       where t.ValueDate.Value.Date >= fromDate.Value.Date && t.ValueDate.Value.Date <= toDate.Value.Date
                                       && t.IsActive == true 
                                       && sa.ParentAccountId == parent.Id
                                       group td by new { sa.Id, sa.AccountNo, sa.AccountName } into g
                                       select new TrialBalanceItem
                                       {
                                           Id = g.Key.Id,
                                           AccountNo = g.Key.AccountNo,
                                           AccountName = g.Key.AccountName,
                                           IsGroup = false,
                                           Balance = g.Sum(x => x.DrCrAmount)
                                       }).ToListAsync();

                parent.Childrens.AddRange(_accGroups);
                parent.Childrens.AddRange(_accounts);

                await GetChildrens(fromDate, toDate, _accGroups);

                parent.Balance = parent.Childrens.Sum(x => x.Balance);
            }
        }

        return true;
    }
    
    public async Task<IEnumerable<LedgerReportViewModel>> LedgerReport(int accountId, DateTime fromDate, DateTime toDate)
    {
        var data = new List<LedgerReportViewModel>();

        var financialYear = await _db.FinancialYears.OrderBy(x => x.YearStartDate).LastOrDefaultAsync();

        if (financialYear == null)
        {
            financialYear = await _db.FinancialYears.Where(x => x.IsActive == true).OrderBy(x => x.YearStartDate).LastAsync();
        }

        var opeingBalance = await (from t in _db.Transactions
                                   join td in _db.TransactionDetails on t.Id equals td.TransactionId
                                   where t.ValueDate.Value.Date >= financialYear.YearStartDate.Date && t.ValueDate.Value.Date < fromDate.Date
                                   && td.AccountId == accountId
                                  
                                   select td.DrCrAmount).SumAsync();

        data.Add(new LedgerReportViewModel
        {
            Particular = "Opening Balance",
            DrCrAmount = opeingBalance
        });

        var transactions = await (from t in _db.Transactions
                                  join td in _db.TransactionDetails on t.Id equals td.TransactionId
                                  join sa in _db.SubsidiaryAccounts on td.AccountId equals sa.Id
                                  join pa in _db.ParentAccounts on sa.ParentAccountId equals pa.Id
                                  join v in _db.VoucherTypes on t.VoucherTypeId equals v.Id
                                  join c in _db.CostCenters on td.CostCenterId equals c.Id
                                  where t.ValueDate.Value.Date >= fromDate.Date
                                    && t.ValueDate.Value.Date <= toDate.Date
                                    && td.AccountId == accountId
                                    && t.IsActive == true 
                                  select new LedgerReportViewModel
                                  {
                                      TransactionId = t.Id,
                                      TransactionNo = t.TransactionNo,
                                      ValueDate = t.ValueDate,
                                      GeneralParticular = t.GeneralParticular,
                                      VoucherTypeName = v.Name,

                                      CostCenterName = c.Name,
                                      AccountId = sa.Id,
                                      AccountNo = sa.AccountNo,
                                      AccountName = sa.AccountName,
                                      Particular = td.Particular,
                                      DrCrAmount = td.DrCrAmount,
                                      InvoiceId = t.InvoiceId,
                                  }).ToListAsync();

        foreach (var item in transactions)
        {
            if (String.IsNullOrEmpty(item.Particular))
            {
                var accounts = await (from t in _db.Transactions
                                      join td in _db.TransactionDetails on t.Id equals td.TransactionId
                                      join a in _db.SubsidiaryAccounts on td.AccountId equals a.Id
                                      where t.Id == item.TransactionId && td.AccountId != item.AccountId
                                      select a.AccountName).Distinct().ToListAsync();

                item.Particular = String.Join(",", accounts);
            }
        }

        data.AddRange(transactions);

        data = data.OrderBy(x => x.ValueDate).ToList();

        decimal preClosingBalance = 0;
        foreach (var item in data)
        {
            item.ClosingBalance = preClosingBalance + item.DrCrAmount;
            preClosingBalance = item.ClosingBalance;
        }

        return data;
    }

}
