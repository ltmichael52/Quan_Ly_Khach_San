using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Team_Project_4.Models
{
    public partial class HotelDbContext : DbContext
    {
        public HotelDbContext()
        {
        }

        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hoadon> Hoadons { get; set; } = null!;
        public virtual DbSet<Khach> Khaches { get; set; } = null!;
        public virtual DbSet<Nhanvien> Nhanviens { get; set; } = null!;
        public virtual DbSet<Phieuthue> Phieuthues { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MICHAEL;Initial Catalog=QLKHACHSAN2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.Mahd);

                entity.ToTable("HOADON");

                entity.Property(e => e.Mahd)
                    .HasMaxLength(7)
                    .HasColumnName("MAHD")
                    .IsFixedLength();

                entity.Property(e => e.Makh)
                    .HasMaxLength(7)
                    .HasColumnName("MAKH")
                    .IsFixedLength();

                entity.Property(e => e.Manv)
                    .HasMaxLength(7)
                    .HasColumnName("MANV")
                    .IsFixedLength();

                entity.Property(e => e.Mapt)
                    .HasMaxLength(7)
                    .HasColumnName("MAPT")
                    .IsFixedLength();

                entity.Property(e => e.Songayo).HasColumnName("SONGAYO");

                entity.Property(e => e.Tongtien).HasColumnName("TONGTIEN");

                entity.HasOne(d => d.MakhNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.Makh)
                    .HasConstraintName("FK_HOADON_KHACH");

                entity.HasOne(d => d.ManvNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.Manv)
                    .HasConstraintName("FK_HOADON_NHANVIEN");

                entity.HasOne(d => d.MaptNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.Mapt)
                    .HasConstraintName("FK_HOADON_PHIEUTHUE");
            });

            modelBuilder.Entity<Khach>(entity =>
            {
                entity.HasKey(e => e.Makh);

                entity.ToTable("KHACH");

                entity.Property(e => e.Makh)
                    .HasMaxLength(7)
                    .HasColumnName("MAKH")
                    .IsFixedLength();

                entity.Property(e => e.Cmndkh)
                    .HasMaxLength(15)
                    .HasColumnName("CMNDKH");

                entity.Property(e => e.Diachikh)
                    .HasMaxLength(100)
                    .HasColumnName("DIACHIKH");

                entity.Property(e => e.Loaikhach)
                    .HasMaxLength(10)
                    .HasColumnName("LOAIKHACH")
                    .IsFixedLength();

                entity.Property(e => e.Tel)
                    .HasMaxLength(12)
                    .HasColumnName("TEL");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(30)
                    .HasColumnName("TENKH");

                entity.Property(e => e.Tuoi).HasColumnName("TUOI");
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.Manv);

                entity.ToTable("NHANVIEN");

                entity.Property(e => e.Manv)
                    .HasMaxLength(7)
                    .HasColumnName("MANV")
                    .IsFixedLength();

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("DIACHI");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(30)
                    .HasColumnName("HOTEN");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("NGAYSINH");

                entity.Property(e => e.Phai)
                    .HasMaxLength(3)
                    .HasColumnName("PHAI")
                    .IsFixedLength();

                entity.Property(e => e.Sdt)
                    .HasMaxLength(12)
                    .HasColumnName("SDT");
            });

            modelBuilder.Entity<Phieuthue>(entity =>
            {
                entity.HasKey(e => e.Mapt)
                    .HasName("PK_Table_1");

                entity.ToTable("PHIEUTHUE");

                entity.Property(e => e.Mapt)
                    .HasMaxLength(7)
                    .HasColumnName("MAPT")
                    .IsFixedLength();

                entity.Property(e => e.Makh)
                    .HasMaxLength(7)
                    .HasColumnName("MAKH")
                    .IsFixedLength();

                entity.Property(e => e.Map).HasColumnName("MAP");

                entity.Property(e => e.Ngaylappt)
                    .HasColumnType("date")
                    .HasColumnName("NGAYLAPPT");

                entity.HasOne(d => d.MakhNavigation)
                    .WithMany(p => p.Phieuthues)
                    .HasForeignKey(d => d.Makh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEUTHUE_KHACH");

                entity.HasOne(d => d.MapNavigation)
                    .WithMany(p => p.Phieuthues)
                    .HasForeignKey(d => d.Map)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEUTHUE_PHONG");
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.Map);

                entity.ToTable("PHONG");

                entity.Property(e => e.Map)
                    .ValueGeneratedNever()
                    .HasColumnName("MAP");

                entity.Property(e => e.Dongia).HasColumnName("DONGIA");

                entity.Property(e => e.Ghichu)
                    .HasColumnType("text")
                    .HasColumnName("GHICHU");

                entity.Property(e => e.Loai)
                    .HasMaxLength(30)
                    .HasColumnName("LOAI");

                entity.Property(e => e.Tenphong)
                    .HasMaxLength(30)
                    .HasColumnName("TENPHONG");

                entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
