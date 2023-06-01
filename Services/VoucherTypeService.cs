using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class VoucherTypeService
{
    private ERPContext _db;
    private IMapper _mapper;
    public VoucherTypeService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VoucherTypeViewModel>> Get(int pageIndex, int pageSize)
    {
        return await _mapper.ProjectTo<VoucherTypeViewModel>(
            _db.VoucherTypes
            .Where(u => u.IsActive == true))
            .OrderByDescending(o => o.Id)
           .Skip((pageIndex - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
    }

    public async Task<VoucherTypeViewModel> GetByID(int id)
    {
        return _mapper.Map<VoucherTypeViewModel>(
            await _db.VoucherTypes
                .Where(b => b.Id == id && b.IsActive == true )
                .SingleOrDefaultAsync());
    }

    public async Task<int> Create(VoucherTypeViewModel VoucherTypeVM)
    {
        await _db.VoucherTypes.AddAsync(_mapper.Map<VoucherType>(VoucherTypeVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> CreateList(IList<VoucherTypeViewModel> voucherTypeVM)
    {
        await _db.VoucherTypes.AddRangeAsync(_mapper.ProjectTo<VoucherType>(voucherTypeVM.AsQueryable()));

        return await _db.SaveChangesAsync();
    }

    public async Task<int> Update(VoucherTypeViewModel VoucherTypeVM)
    {
        _db.VoucherTypes.Update(_mapper.Map<VoucherType>(VoucherTypeVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, string userName)
    {
        var data = await _db.VoucherTypes.SingleOrDefaultAsync(x => x.Id == id);
        data.IsActive = false;
        data.ModifiedBy = userName;
        _db.VoucherTypes.Update(data);
        return await _db.SaveChangesAsync();
    }

    public bool IsUnique(VoucherTypeViewModel VoucherTypeVM)
    {
        IQueryable<int> result;

        if (VoucherTypeVM.Id == 0)
        {
            result = (from j in _db.VoucherTypes
                      where j.Name.ToLower() == VoucherTypeVM.Name.ToLower()
                      select j.Id);
        }
        else
        {
            result = (from j in _db.VoucherTypes
                      where j.Name.ToLower().Trim() == VoucherTypeVM.Name.ToLower().Trim() && j.Id != VoucherTypeVM.Id
                      select j.Id);
        }

        if (result.Count() > 0)
        {
            return true;
        }
        return false;

    }
}

