using System.ComponentModel.DataAnnotations;

namespace ScopoERP.Models;

public class SubsidiaryAccount : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string AccountNo { get; set; }
    public string AccountName { get; set; }
    public int? ParentAccountId { get; set; }
    public int AccountTypeId { get; set; }
    public decimal? MonthlyBudget { get; set; } 
    public decimal OpeningBalance { get; set; } 
}