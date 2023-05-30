using System.ComponentModel.DataAnnotations;

namespace ScopoERP.Models
{
    public class VoucherType:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
