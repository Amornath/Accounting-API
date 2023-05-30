using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable
namespace ScopoERP.Models
{
    public class BaseEntity
    {
        [StringLength(20)]
        public string? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastModifiedDate { get; set; }
        [DefaultValue(true)]
        public bool? IsActive { get; set; }
        public int Status { get; set; }
    }
}
