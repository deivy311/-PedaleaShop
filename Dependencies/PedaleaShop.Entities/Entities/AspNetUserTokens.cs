namespace PedaleaShop.Entities.Dtos

{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AspNetUserTokens
    {
        [StringLength(450)]
        public string Id { get; set; }

        public int Version { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [StringLength(450)]
        public string Use { get; set; }

        [Required]
        [StringLength(100)]
        public string Algorithm { get; set; }

        public bool IsX509Certificate { get; set; }

        public bool DataProtected { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
