namespace ScopoERP.ViewModels
{
    public class SubsidiaryAccountViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public int? ParentAccountId { get; set; }
        public string ParentAccountNo { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public decimal? MonthlyBudget { get; set; }
        public decimal OpeningBalance { get; set; }
    }
}
