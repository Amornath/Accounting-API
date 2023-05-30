using ScopoERP.Models;
using ScopoERP.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ScopoERP.Services;

public class DropDownService
{
    private ERPContext _db;

    public DropDownService(ERPContext db)
    {
        _db = db;
    }

    public async Task<List<DropDownViewModel>> GetDropDownList(string type, int? accountTypeId)
    {
        List<DropDownViewModel> dropDownList = new List<DropDownViewModel>();

        switch (type)
        {

            case "AccountType":
                dropDownList = await _db.AccountTypes.Select(r => new DropDownViewModel { Value = r.Id, Label = r.Name }).ToListAsync();
                break;
            case "ParentAccount":
                dropDownList = await _db.ParentAccounts.Where(w =>  w.IsActive == true).Select(r => new DropDownViewModel { Value = r.Id, Label = r.AccountNo +"-"+r.AccountName }).ToListAsync();
                break;
            case "CostCenter":
                dropDownList = await _db.CostCenters.Where(w =>  w.IsActive == true).Select(r => new DropDownViewModel { Value = r.Id, Label = r.Name }).ToListAsync();
                break; 
            case "SubsidiaryAccount":
                dropDownList = await _db.SubsidiaryAccounts.Where(w =>  w.IsActive == true && (accountTypeId == null ? w.AccountTypeId == w.AccountTypeId : w.AccountTypeId == accountTypeId)).Select(r => new DropDownViewModel { Value = r.Id, Label = r.AccountNo +"-"+ r.AccountName }).ToListAsync();
                break; 
            case "VoucherType":
                dropDownList = await _db.VoucherTypes.Where(w =>  w.IsActive == true).Select(r => new DropDownViewModel { Value = r.Id, Label = r.Name }).ToListAsync();
                break;
            case "Transaction":
                dropDownList = await _db.Transactions.Where(w =>  w.IsActive == true).Select(r => new DropDownViewModel { Value = r.Id, Label = r.TransactionNo }).ToListAsync();
                break;
            default:
                break;
        }

        return dropDownList;
    }
}
