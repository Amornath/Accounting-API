namespace ScopoERP.ViewModels
{
    public class ParentAccountViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }    
        public string AccountName { get; set; }
        public string ParentAccountNo { get; set; }
        public string ParentAccountName { get; set; }
        public int? ParentId { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
    }
}
