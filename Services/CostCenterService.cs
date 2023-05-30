using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class CostCenterService
{
    private ERPContext _db;
    private IMapper _mapper;
    public CostCenterService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CostCenterViewModel>> Get(int pageIndex, int pageSize)
    {
        return await _mapper.ProjectTo<CostCenterViewModel>(
            _db.CostCenters
            .Where(u => u.IsActive == true))
            .OrderByDescending(o => o.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<CostCenterViewModel> GetByID(int id)
    {
        return _mapper.Map<CostCenterViewModel>(
            await _db.CostCenters
                .Where(b => b.Id == id && b.IsActive == true )
                .SingleOrDefaultAsync());
    }

    public async Task<int> Create(CostCenterViewModel CostCenterVM)
    {
        await _db.CostCenters.AddAsync(_mapper.Map<CostCenter>(CostCenterVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Update(CostCenterViewModel CostCenterVM)
    {
        _db.CostCenters.Update(_mapper.Map<CostCenter>(CostCenterVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, string userName)
    {
        var data = await _db.CostCenters.SingleOrDefaultAsync(x => x.Id == id);
        data.IsActive = false;
        data.ModifiedBy = userName;
        _db.CostCenters.Update(data);
        return await _db.SaveChangesAsync();
    }
}

