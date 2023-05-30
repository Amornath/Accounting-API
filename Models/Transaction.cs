using System.ComponentModel.DataAnnotations;

namespace ScopoERP.Models;

public class Transaction : BaseEntity
{
    public Transaction()
    {
        TransactionDetails = new HashSet<TransactionDetails>();
    }
    [Key]
    public int Id { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ValueDate { get; set; }
    public string GeneralParticular { get; set; }
    public string TransactionNo { get; set; }
    public int? InvoiceId { get; set; }
    public int? BillId { get; set; }
    public int? VoucherTypeId { get; set; }
    public int? SalesReturnId { get; set; }
    public int? PurchaseReturnId { get; set; }
    public ICollection<TransactionDetails> TransactionDetails { get; set; }
}

public class TransactionDetails
{
    [Key]
    public int Id { get; set; }
    public int TransactionId { get; set; }
    public int CostCenterId { get; set; }
    public int AccountId { get; set; }
    public decimal DrCrAmount { get; set; }
    public string Particular { get; set; }

    public virtual Transaction Transaction { get; set; }
}