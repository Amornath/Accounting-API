namespace ScopoERP.ViewModels
{
    public class FinancialYearViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public DateTime YearStartDate { get; set; }
        public DateTime YearEndDate { get; set; }
        public int? TransactionId { get; set; }
        public int? BookClosingAccount { get; set; }
        public decimal? BookClosingAmount { get; set; }
    }
}
