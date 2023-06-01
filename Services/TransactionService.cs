using ScopoERP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScopoERP.ViewModels;

namespace ScopoERP.Services;

public class TransactionService
{
    private ERPContext _db;
    private IMapper _mapper;
    public TransactionService(ERPContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionViewModel>> Get(int pageIndex, int pageSize, bool excelExport, int? transactionId, string voucherTypeName, DateTime? fromDate, DateTime? toDate)
    {
        var result = (from s in _db.Transactions
                            join v in _db.VoucherTypes on s.VoucherTypeId equals v.Id
                            where s.IsActive == true 
                            && (transactionId == null ? s.Id == s.Id : s.Id == transactionId)
                            && (voucherTypeName == null ? v.Name == v.Name : v.Name == voucherTypeName)
                            && (fromDate == null ? s.ValueDate == s.ValueDate : s.ValueDate >= fromDate)
                            && (toDate == null ? s.ValueDate == s.ValueDate : s.ValueDate <= toDate)
                            select new TransactionViewModel
                            {
                                Id = s.Id, 
                                ValueDate = s.ValueDate,
                                TransactionNo = s.TransactionNo,  
                                VoucherTypeId=s.VoucherTypeId,
                                VoucherTypeName =v.Name,
                                GeneralParticular =s.GeneralParticular,
                               
                            });
        if (excelExport == false)
        {
            result = result.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        else
        {
            result = result.OrderByDescending(o => o.Id);
        }
        var list = await result.ToListAsync();

        if (list.Count > 0)
        {
            foreach (var item in list)
            {
                item.TransactionDetails = await (from s in _db.TransactionDetails
                                                   join sb in _db.SubsidiaryAccounts on s.AccountId equals sb.Id
                                                   join c in _db.CostCenters on s.CostCenterId equals c.Id
                                                   where s.TransactionId == item.Id
                                                   select new TransactionDetailsViewModel
                                                   {
                                                       Id = s.Id,
                                                       Particular = s.Particular,
                                                       DrCrAmount = s.DrCrAmount,                                                 
                                                       AccountId = s.AccountId,                                                 
                                                       AccountName = sb.AccountName,                                                 
                                                       AccountNo = sb.AccountNo,                                                 
                                                       CostCenterId = s.CostCenterId,                                                 
                                                       CostCenterName = c.Name,                                                 

                                                   }).ToListAsync();
            }
        }
        return list;
    }

    public async Task<TransactionViewModel> GetByID(int id)
    {
        return await _mapper.ProjectTo<TransactionViewModel>(_db.Transactions.Where(w => w.Id == id && w.IsActive == true )).SingleOrDefaultAsync();
    }

    public async Task<string> Create(TransactionViewModel TransactionVM)
    {
        if(TransactionVM.TransactionNo == null)
        {
            TransactionVM.TransactionNo = await GetTransactionNo();
        }
        TransactionVM.VoucherTypeId= _db.VoucherTypes.Where(w => w.Name == TransactionVM.VoucherTypeName).Select(s => s.Id).FirstOrDefault();
        
        await _db.Transactions.AddAsync(_mapper.Map<Transaction>(TransactionVM));
        await _db.SaveChangesAsync();
        return TransactionVM.TransactionNo;
    }

    public async Task<int> Update(TransactionViewModel TransactionVM)
    {
        if (TransactionVM.TransactionNo == null)
        {
            TransactionVM.TransactionNo = await GetTransactionNo();
        }
        _db.Transactions.Update(_mapper.Map<Transaction>(TransactionVM));
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, string userName)
    {
        var data = await _db.Transactions.SingleOrDefaultAsync(x => x.Id == id);
        data.IsActive = false;
        data.ModifiedBy = userName;
        _db.Transactions.Update(data);
        return await _db.SaveChangesAsync();
    }
    public async Task<string> GetTransactionNo()
    {


        string newTransactionNo = string.Empty;

        var lastReference = await _db.References.OrderByDescending(d => d.LastTransactionNo).FirstOrDefaultAsync();


        string newDONoInDigit = (Convert.ToInt32(lastReference.LastTransactionNo.Split('-').Last()) + 1).ToString().PadLeft(5, '0');

        newTransactionNo = "TR-" + DateTime.Now.Year.ToString() + "-" + newDONoInDigit;

        lastReference.LastTransactionNo = newTransactionNo;
        _db.References.Update(lastReference);
        await _db.SaveChangesAsync();

        return newTransactionNo;
    }

    public async Task<string> CreateAutoJournal(Transaction transaction)
    {
        transaction.VoucherTypeId = _db.VoucherTypes.Where(w=> w.Name == "JournalVoucher").Select(s => s.Id).FirstOrDefault();
        transaction.TransactionNo = await GetTransactionNo();

        await _db.Transactions.AddAsync(transaction);
        await _db.SaveChangesAsync();
        return transaction.TransactionNo;
    }
}


