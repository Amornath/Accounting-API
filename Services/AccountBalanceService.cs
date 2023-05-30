using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class AccountBalanceService
{
    private ERPContext _db;
    private IMapper _mapper;
    public AccountBalanceService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountBalanceViewModel>> Get(int pageIndex, int pageSize)
    {
        return await _mapper.ProjectTo<AccountBalanceViewModel>(
            _db.AccountBalances
            .Where(u => u.IsActive == true))
            .OrderByDescending(o => o.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<AccountBalanceViewModel> GetByID(int id)
    {
        return _mapper.Map<AccountBalanceViewModel>(
            await _db.AccountBalances
                .Where(b => b.Id == id && b.IsActive == true)
                .SingleOrDefaultAsync());
    }

    public async Task<int> Create(AccountBalanceViewModel AccountBalanceVM)
    {
        await _db.AccountBalances.AddAsync(_mapper.Map<AccountBalance>(AccountBalanceVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Update(AccountBalanceViewModel AccountBalanceVM)
    {
        _db.AccountBalances.Update(_mapper.Map<AccountBalance>(AccountBalanceVM));
        return await _db.SaveChangesAsync();
    }

}

