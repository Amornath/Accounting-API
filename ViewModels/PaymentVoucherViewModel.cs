namespace ScopoERP.ViewModels
{
    public class PaymentVoucherViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ValueDate { get; set; }
        public string GeneralParticular { get; set; }
        public string TransactionNo { get; set; }
        public int? VoucherTypeId { get; set; }
        public int SupplierId { get; set; }
        public int PaymentAccountId { get; set; }

        public decimal PaidAmount { get; set; }
        public string VoucherTypeName { get; set; }
    }
}
