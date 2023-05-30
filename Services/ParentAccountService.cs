using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class ParentAccountService
{
    private ERPContext _db;
    private IMapper _mapper;
    public ParentAccountService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ParentAccountViewModel>> Get(int pageIndex, int pageSize,bool excelExport, int? accountTypeId,int? parentaccountId)
    {

        var result = (from s in _db.ParentAccounts
                            join p in _db.ParentAccounts on s.ParentId equals p.Id into pe from p in pe.DefaultIfEmpty()
                            join d in _db.AccountTypes
                            on s.AccountTypeId equals d.Id into df
                            from d in df.DefaultIfEmpty()
                            where s.IsActive == true 
                           && (accountTypeId == null ? s.AccountTypeId == s.AccountTypeId : s.AccountTypeId == accountTypeId)
                            && (parentaccountId == null ? s.Id == s.Id : s.Id == parentaccountId)
                      select new ParentAccountViewModel
                            {
                                Id = s.Id,
                                AccountNo = s.AccountNo,
                                AccountName = s.AccountName,
                                ParentId=s.ParentId,
                                ParentAccountNo=p.AccountNo,
                                ParentAccountName=p.AccountName,
                                AccountTypeId=s.AccountTypeId,
                                AccountTypeName = d.Name

                            });

        if (excelExport == false)
        {
            result = result.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        return await result.ToListAsync();

    }

    public async Task<ParentAccountViewModel> GetByID(int id)
    {
        return _mapper.Map<ParentAccountViewModel>(
            await _db.ParentAccounts
                .Where(b => b.Id == id && b.IsActive == true)
                .SingleOrDefaultAsync());
    }

    public async Task<int> Create(ParentAccountViewModel ParentAccountVM)
    {
        ParentAccountVM.AccountNo = await GetAccountNo();
        await _db.ParentAccounts.AddAsync(_mapper.Map<ParentAccount>(ParentAccountVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Update(ParentAccountViewModel ParentAccountVM)
    {
        _db.ParentAccounts.Update(_mapper.Map<ParentAccount>(ParentAccountVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, string userName)
    {
        var data = await _db.ParentAccounts.SingleOrDefaultAsync(x => x.Id == id);
        data.IsActive = false;
        data.ModifiedBy = userName;
        _db.ParentAccounts.Update(data);
        return await _db.SaveChangesAsync();
    }
    public async Task<string> GetAccountNo()
    {
        string newDONo = string.Empty;

        var result = await _db.ParentAccounts.OrderByDescending(d => d.Id).Select(s => s.AccountNo).FirstOrDefaultAsync();

        if (result == null)
        {
            newDONo ="10001";
        }
        else
        {
           
            newDONo = (Convert.ToInt32(result) + 1).ToString();
        }
        return newDONo;
    }
}

