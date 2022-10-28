using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LB1.Models
{
    public partial class Order
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Client { get; set; } = null!;
        [StringLength(50)]
        public string Address { get; set; } = null!;
        [StringLength(50)]
        public string Contact { get; set; } = null!;
        public int PhoneId { get; set; }

        [ForeignKey("PhoneId")]
        [InverseProperty("Orders")]
        public virtual Phone Phone { get; set; } = null!;
    }
}
