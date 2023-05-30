using System.ComponentModel.DataAnnotations;

namespace ScopoERP.Models;

public class AccountType : BaseEntity
{  
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
   
}