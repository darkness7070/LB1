using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LB1.Models
{
    public partial class Phone
    {
        public Phone()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Company { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        [InverseProperty("Phone")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
