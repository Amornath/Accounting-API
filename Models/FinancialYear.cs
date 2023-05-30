using System.ComponentModel.DataAnnotations;

namespace ScopoERP.Models;

public class FinancialYear : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime YearStartDate { get; set; }
    public DateTime YearEndDate { get; set; }
    public int? TransactionId { get; set; }
    public int? BookClosingAccount { get; set; }
    public decimal? BookClosingAmount { get; set; }
}