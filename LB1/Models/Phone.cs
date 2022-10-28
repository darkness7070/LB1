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
        [Display(Name = "Модель")]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "Производитель")]
        public string Company { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Column("idPhoto")]
        public int IdPhoto { get; set; }

        [ForeignKey("IdPhoto")]
        [InverseProperty("Phones")]
        [Display(Name = "Фото")]
        public virtual Image IdPhotoNavigation { get; set; } = null!;
        [InverseProperty("Phone")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
