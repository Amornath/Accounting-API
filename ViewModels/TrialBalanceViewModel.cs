namespace ScopoERP.ViewModels
{
    public class TrialBalanceViewModel
    {
        public TrialBalanceViewModel()
        {
            Childrens = new List<TrialBalanceItem>();
        }

        public string AccountType { get; set; }

        public List<TrialBalanceItem> Childrens { get; set; }
    }


    public class TrialBalanceItem
    {
        public TrialBalanceItem()
        {
            Childrens = new List<TrialBalanceItem>();
        }

        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public bool IsGroup { get; set; }

        public List<TrialBalanceItem> Childrens { get; set; }
    }
}