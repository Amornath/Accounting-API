namespace ScopoERP.ViewModels
{
    public class BalanceSheetViewModel
    {
        public BalanceSheetViewModel()
        {
            Childrens = new List<TrialBalanceItem>();
        }

        public string AccountType { get; set; }

        public List<TrialBalanceItem> Childrens { get; set; }
    }
  
}