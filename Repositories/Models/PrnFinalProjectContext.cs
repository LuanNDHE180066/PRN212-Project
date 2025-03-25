using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Models;

public partial class PrnFinalProjectContext : DbContext
{
    public static PrnFinalProjectContext Ins = new PrnFinalProjectContext();

    public PrnFinalProjectContext()
    {
        if (Ins == null) Ins = this;

    }

    public PrnFinalProjectContext(DbContextOptions<PrnFinalProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<Expenditure> Expenditures { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<GoodType> GoodTypes { get; set; }

    public virtual DbSet<HistoryBuyGood> HistoryBuyGoods { get; set; }

    public virtual DbSet<HistoryUsedDevice> HistoryUsedDevices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<WorkingHistory> WorkingHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PRN_FinalProject; Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__Customer__D837D05FEA990B15");

            entity.ToTable("Customer");

            entity.Property(e => e.Cid).HasColumnName("cid");
            entity.Property(e => e.CName)
                .HasMaxLength(255)
                .HasColumnName("cName");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Did).HasName("PK__Device__D877D216FD722395");

            entity.ToTable("Device");

            entity.Property(e => e.Did).HasColumnName("did");
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            entity.HasOne(d => d.Type).WithMany(p => p.Devices)
                .HasForeignKey(d => d.Typeid)
                .HasConstraintName("FK__Device__typeid__4E88ABD4");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.DtId).HasName("PK__DeviceTy__24BA177CB12AF2E6");

            entity.ToTable("DeviceType");

            entity.Property(e => e.DtId).HasColumnName("dtID");
            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.DtName)
                .HasMaxLength(255)
                .HasColumnName("dtName");
        });

        modelBuilder.Entity<Expenditure>(entity =>
        {
            entity.HasKey(e => e.ExId).HasName("PK__Expendit__38F47E7814CFB175");

            entity.ToTable("Expenditure");

            entity.Property(e => e.ExId).HasColumnName("exID");
            entity.Property(e => e.GoodsId).HasColumnName("goodsID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Supplier).HasMaxLength(255);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Goods).WithMany(p => p.Expenditures)
                .HasForeignKey(d => d.GoodsId)
                .HasConstraintName("FK__Expenditu__goods__5FB337D6");

            entity.HasOne(d => d.Staff).WithMany(p => p.Expenditures)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Expenditu__Staff__60A75C0F");
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("PK__Goods__DCD80EF815BFC192");

            entity.Property(e => e.Gid).HasColumnName("gid");
            entity.Property(e => e.GName)
                .HasMaxLength(255)
                .HasColumnName("gName");
            entity.Property(e => e.Img).HasMaxLength(255);
            entity.Property(e => e.Typeid).HasColumnName("typeid");
            entity.Property(e => e.Unit).HasMaxLength(255);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Type).WithMany(p => p.Goods)
                .HasForeignKey(d => d.Typeid)
                .HasConstraintName("FK__Goods__typeid__5441852A");
        });

        modelBuilder.Entity<GoodType>(entity =>
        {
            entity.HasKey(e => e.Gtid).HasName("PK__GoodType__4AB47E5F4C6449CB");

            entity.ToTable("GoodType");

            entity.Property(e => e.Gtid).HasColumnName("gtid");
            entity.Property(e => e.GtName)
                .HasMaxLength(255)
                .HasColumnName("gtName");
        });

        modelBuilder.Entity<HistoryBuyGood>(entity =>
        {
            entity.HasKey(e => e.HbgId).HasName("PK__HistoryB__5CCDA8587CDE86BE");

            entity.Property(e => e.HbgId).HasColumnName("hbgID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.GoodsId).HasColumnName("GoodsID");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

            entity.HasOne(d => d.Goods).WithMany(p => p.HistoryBuyGoods)
                .HasForeignKey(d => d.GoodsId)
                .HasConstraintName("FK__HistoryBu__Goods__6C190EBB");

            entity.HasOne(d => d.Invoice).WithMany(p => p.HistoryBuyGoods)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__HistoryBu__Invoi__6B24EA82");
        });

        modelBuilder.Entity<HistoryUsedDevice>(entity =>
        {
            entity.HasKey(e => e.HudId).HasName("PK__HistoryU__6B6A0FBEA57E47B4");

            entity.ToTable("HistoryUsedDevice");

            entity.Property(e => e.HudId).HasColumnName("hudID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DeviceId).HasColumnName("DeviceID");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

            entity.HasOne(d => d.Device).WithMany(p => p.HistoryUsedDevices)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK__HistoryUs__Devic__68487DD7");

            entity.HasOne(d => d.Invoice).WithMany(p => p.HistoryUsedDevices)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__HistoryUs__Invoi__6754599E");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PK__Invoice__DC512D729EB55399");

            entity.ToTable("Invoice");

            entity.Property(e => e.IId).HasColumnName("iID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Invoice__Custome__6383C8BA");

            entity.HasOne(d => d.Staff).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Invoice__StaffID__6477ECF3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("PK__Role__C2B7EDE8FDFE5E89");

            entity.ToTable("Role");

            entity.Property(e => e.Rid).HasColumnName("rid");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RName)
                .HasMaxLength(255)
                .HasColumnName("rName");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("PK__Staff__DDDFDD3698D59AA3");

            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.SName)
                .HasMaxLength(255)
                .HasColumnName("sName");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK__Staff__roleid__59FA5E80");
        });

        modelBuilder.Entity<WorkingHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkingH__3213E83F7F4E2DF2");

            entity.ToTable("WorkingHistory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");

            entity.HasOne(d => d.Staff).WithMany(p => p.WorkingHistories)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__WorkingHi__Staff__6E01572D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
