using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LB1.Models;

namespace LB1.data
{
    public partial class MVCMobileContext : DbContext
    {
        public MVCMobileContext()
        {
        }

        public MVCMobileContext(DbContextOptions<MVCMobileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Phone> Phones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.126.14,3334;Database=MVCMobile;user=sa;password=P@ssw0rd;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Phone)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PhoneId)
                    .HasConstraintName("FK_Orders_Phones");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasOne(d => d.IdPhotoNavigation)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.IdPhoto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phones_images");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
