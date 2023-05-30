using System.ComponentModel.DataAnnotations;

namespace ScopoERP.Models
{
    public class ReferenceNo
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string LastTransactionNo { get; set; }
    }
}
