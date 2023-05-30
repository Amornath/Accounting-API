namespace ScopoERP.ViewModels
{
    public class LedgerReportViewModel
    {
        public int TransactionId { get; set; }
        public string TransactionNo { get; set; }

        public DateTime? ValueDate { get; set; }
        public string GeneralParticular { get; set; }    
        public string VoucherTypeName { get; set; }
        public string CostCenterName { get; set; }

        public int AccountId { get; set; }
        public int? InvoiceId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal DrCrAmount { get; set; }
        public decimal ClosingBalance { get; set; }
        public string Particular { get; set; }
    }
}
