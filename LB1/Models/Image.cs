using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LB1.Models
{
    [Table("images")]
    public partial class Image
    {
        public Image()
        {
            Phones = new HashSet<Phone>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("path")]
        [StringLength(255)]
        public string Path { get; set; } = null!;

        [InverseProperty("IdPhotoNavigation")]
        public virtual ICollection<Phone> Phones { get; set; }
    }
}
