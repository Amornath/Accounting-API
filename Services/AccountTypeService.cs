using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class AccountTypeService
{
    private ERPContext _db;
    private IMapper _mapper;
    public AccountTypeService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountTypeViewModel>> Get(int pageIndex, int pageSize)
    {
        return await _mapper.ProjectTo<AccountTypeViewModel>(
            _db.AccountTypes
            .Where(u => u.IsActive == true ))
            .OrderByDescending(o => o.Id)
            .Skip((pageIndex - 1) * pageSize)          
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<AccountTypeViewModel> GetByID(int id)
    {
        return _mapper.Map<AccountTypeViewModel>(
            await _db.AccountTypes
                .Where(b => b.Id == id && b.IsActive == true )
                .SingleOrDefaultAsync());
    }

    public async Task<int> Create(AccountTypeViewModel AccountTypeVM)
    {
        await _db.AccountTypes.AddAsync(_mapper.Map<AccountType>(AccountTypeVM));
        return await _db.SaveChangesAsync();
    }
    public async Task<int> CreateList(IList<AccountTypeViewModel> accountTypeVM)
    {
        await _db.AccountTypes.AddRangeAsync(_mapper.ProjectTo<AccountType>(accountTypeVM.AsQueryable()));

        return await _db.SaveChangesAsync();
    }
    public async Task<int> Update(AccountTypeViewModel AccountTypeVM)
    {
        _db.AccountTypes.Update(_mapper.Map<AccountType>(AccountTypeVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, string userName)
    {
        var data = await _db.AccountTypes.SingleOrDefaultAsync(x => x.Id == id);
        data.IsActive = false;
        data.ModifiedBy = userName;
        _db.AccountTypes.Update(data);
        return await _db.SaveChangesAsync();
    }

    public bool IsUnique(AccountTypeViewModel AccountTypeVM)
    {
        IQueryable<int> result;

        if (AccountTypeVM.Id == 0)
        {
            result =(from j in _db.AccountTypes
                    where j.Name.ToLower() == AccountTypeVM.Name.ToLower()
                    select j.Id);
        }
        else
        {
            result = (from j in _db.AccountTypes
                      where j.Name.ToLower().Trim() == AccountTypeVM.Name.ToLower().Trim() && j.Id != AccountTypeVM.Id 
                      select j.Id);
        }

        if (result.Count() > 0)
        {
            return true;
        }
        return false;

    }
}

