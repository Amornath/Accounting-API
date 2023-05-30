namespace ScopoERP.ViewModels
{
    public class ProfitLossStatementViewModel
    {
        public string ParentAccount { get; set; }
        public string SubsidiaryAccount { get; set; }
        public decimal? DebitBalance { get; set; }
        public decimal? CreditBalance { get; set; }
        public decimal? Balance { get; set; }
    }
}