using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class FinancialYearService
{
    private ERPContext _db;
    private IMapper _mapper;
    public FinancialYearService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FinancialYearViewModel>> Get(int pageIndex, int pageSize, bool excelExport)
    {
        var result = _mapper.ProjectTo<FinancialYearViewModel>(
            _db.FinancialYears
            .Where(u => u.IsActive == true));

        if (excelExport == false)
        {
            result = result.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        return await result.ToListAsync();

    }

    public async Task<FinancialYearViewModel> GetByID(int id)
    {
        return _mapper.Map<FinancialYearViewModel>(
            await _db.FinancialYears
                .Where(b => b.Id == id && b.IsActive == true)
                .SingleOrDefaultAsync());
    }

    public async Task<int> Create(FinancialYearViewModel FinancialYearVM)
    {
        await _db.FinancialYears.AddAsync(_mapper.Map<FinancialYear>(FinancialYearVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Update(FinancialYearViewModel FinancialYearVM)
    {
        _db.FinancialYears.Update(_mapper.Map<FinancialYear>(FinancialYearVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, string userName)
    {
        var data = await _db.FinancialYears.SingleOrDefaultAsync(x => x.Id == id);
        data.IsActive = false;
        data.ModifiedBy = userName;
        _db.FinancialYears.Update(data);
        return await _db.SaveChangesAsync();
    }
}
