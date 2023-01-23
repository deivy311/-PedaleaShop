namespace PedaleaShop.Entities.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DeviceCodes
    {
        [Key]
        [StringLength(200)]
        public string UserCode { get; set; }

        [Column("DeviceCode")]
        [Required]
        [StringLength(200)]
        public string DeviceCode1 { get; set; }

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
        public DateTime Expiration { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
