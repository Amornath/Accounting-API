using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class SubsidiaryAccountService
{
    private ERPContext _db;
    private IMapper _mapper;
    public SubsidiaryAccountService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubsidiaryAccountViewModel>> Get(int pageIndex, int pageSize, bool excelExport, int? accountTypeId,int? parentAccountId, int? accountId)
    {
        var result = (from s in _db.SubsidiaryAccounts
                            join p in _db.ParentAccounts on s.ParentAccountId equals p.Id
                            join d in _db.AccountTypes
                            on s.AccountTypeId equals d.Id into df
                            from d in df.DefaultIfEmpty()
                            where s.IsActive== true
                            && (accountTypeId == null ? s.AccountTypeId == s.AccountTypeId : s.AccountTypeId == accountTypeId)
                            && (parentAccountId == null ? s.ParentAccountId == s.ParentAccountId : s.ParentAccountId == parentAccountId)
                            && (accountId == null ? s.Id == s.Id : s.Id == accountId)
                             select new SubsidiaryAccountViewModel
                            {
                                Id = s.Id,
                                AccountNo = s.AccountNo,
                                AccountName = s.AccountName,
                                AccountTypeId = s.AccountTypeId,
                                AccountTypeName = d.Name,
                                ParentAccountId=s.ParentAccountId,
                                ParentAccountNo=p.AccountNo,
                                OpeningBalance=s.OpeningBalance,
                                MonthlyBudget=s.MonthlyBudget
                               
                            });

        if (excelExport == false)
        {
            result = result.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        return await result.ToListAsync();
    }

    public async Task<SubsidiaryAccountViewModel> GetByID(int id)
    {
        return _mapper.Map<SubsidiaryAccountViewModel>(
            await _db.SubsidiaryAccounts
                .Where(b => b.Id == id && b.IsActive == true)
                .SingleOrDefaultAsync());
    }

    public async Task<int> Create(SubsidiaryAccountViewModel SubsidiaryAccountVM)
    {
        SubsidiaryAccountVM.AccountNo = await GetAccountNo();
        await _db.SubsidiaryAccounts.AddAsync(_mapper.Map<SubsidiaryAccount>(SubsidiaryAccountVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Update(SubsidiaryAccountViewModel SubsidiaryAccountVM)
    {
        _db.SubsidiaryAccounts.Update(_mapper.Map<SubsidiaryAccount>(SubsidiaryAccountVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, string userName)
    {
        var data = await _db.SubsidiaryAccounts.SingleOrDefaultAsync(x => x.Id == id);
        data.IsActive = false;
        data.ModifiedBy = userName;
        _db.SubsidiaryAccounts.Update(data);
        return await _db.SaveChangesAsync();
    }
    public async Task<string> GetAccountNo()
    {
        string newDONo = string.Empty;

        var result = await _db.SubsidiaryAccounts.OrderByDescending(d => d.Id).Select(s => s.AccountNo).FirstOrDefaultAsync();

        if (result == null)
        {
            newDONo = "10001";
        }
        else
        {

            newDONo = (Convert.ToInt32(result) + 1).ToString();
        }
        return newDONo;
    }
}


