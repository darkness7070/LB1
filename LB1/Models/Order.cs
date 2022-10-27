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
        [Display(Name = "Клиент")]
        public string? Client { get; set; }
        [StringLength(50)]
        [Display(Name = "Адрес")]
        public string? Address { get; set; }
        [StringLength(50)]
        [Display(Name = "Контакты")]
        public string? Contact { get; set; }
        [Display(Name = "Телефон")]
        public int PhoneId { get; set; }

        [ForeignKey("PhoneId")]
        [InverseProperty("Orders")]
        [Display(Name = "Телефон")]
        public virtual Phone Phone { get; set; } = null!;
    }
}
