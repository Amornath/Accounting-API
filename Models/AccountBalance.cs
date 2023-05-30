using ScopoERP.Models;
using System.ComponentModel.DataAnnotations;

public class AccountBalance: BaseEntity
{
    [Key]
    public int Id { get; set; }
    public int AccountId { get; set; }
    public DateTime Date { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal ClosingBalance { get; set; }
}
