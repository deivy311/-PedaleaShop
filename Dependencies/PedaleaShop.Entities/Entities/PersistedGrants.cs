using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedaleaShop.Entities.Dtos
{
    public partial class PersistedGrants
    {
        [Key]
        [StringLength(200)]
        public string Key { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(200)]
        public string SubjectId { get; set; }

        [StringLength(100)]
        public string SessionId { get; set; }

        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Expiration { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ConsumedTime { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
