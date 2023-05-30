namespace ScopoERP.ViewModels
{
    public class ReceiptVoucherViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ValueDate { get; set; }
        public string GeneralParticular { get; set; }
        public string TransactionNo { get; set; }
        public int? VoucherTypeId { get; set; }
        public int CustomerId { get; set; }
        public int ReceiptAccountId { get; set; }

        public decimal ReceivedAmount { get; set; }
        public string VoucherTypeName { get; set; }
    }
}
