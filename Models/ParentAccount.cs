using System.ComponentModel.DataAnnotations;

namespace ScopoERP.Models;

public class ParentAccount : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string AccountNo { get; set; }
    public string AccountName { get; set; }
    public int? ParentId { get; set; }
    public int AccountTypeId { get; set; }

}