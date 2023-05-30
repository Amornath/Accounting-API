namespace ScopoERP.ViewModels
{
    public class AccountBalanceViewModel:BaseViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
    }

    public class AccountDailyBalanceViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ClosingBalance { get; set; }
        public string AccountTypeName { get; set; }
    }
}
