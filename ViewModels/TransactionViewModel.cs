namespace ScopoERP.ViewModels
{
    public class TransactionViewModel : BaseViewModel
    {
     
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ValueDate { get; set; }
        public string GeneralParticular { get; set; }
        public string TransactionNo { get; set; }
        public int? VoucherTypeId { get; set; }
        public string VoucherTypeName { get; set; }
        public List<TransactionDetailsViewModel> TransactionDetails { get; set; }

    }

    public class TransactionDetailsViewModel
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenterName { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public decimal DrCrAmount { get; set; }
        public string Particular { get; set; }

       
    }
}
