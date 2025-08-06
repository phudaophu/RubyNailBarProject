using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RubyNailBarWeb.Models;

public partial class NailsDbContext : DbContext
{
    public NailsDbContext()
    {
    }

    public NailsDbContext(DbContextOptions<NailsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BirthdayNotificationLog> BirthdayNotificationLogs { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerPointLog> CustomerPointLogs { get; set; }

    public virtual DbSet<GiftCard> GiftCards { get; set; }

    public virtual DbSet<GiftCardDetailLog> GiftCardDetailLogs { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<TipLog> TipLogs { get; set; }

    public virtual DbSet<TurnLog> TurnLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<WorkDayRecord> WorkDayRecords { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-SRNILOS;Initial Catalog=NailsDb;User ID=sa;Password=123456;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BirthdayNotificationLog>(entity =>
        {
            entity.HasKey(e => e.BirthdayNotificationLogId).HasName("PK__Birthday__D3DD4D5356532BDB");

            entity.ToTable("BirthdayNotificationLog");

            entity.Property(e => e.CouponCode).HasMaxLength(50);
            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.BirthdayNotificationLogs)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BirthdayN__Custo__403A8C7D");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8DBFF758B");

            entity.ToTable("Customer");

            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.CustomerLevel).HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.LastVisitDatetime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Address1).HasMaxLength(255);
            entity.Property(e => e.CivicAddress).HasMaxLength(255);
            entity.Property(e => e.CityName).HasMaxLength(128);
            entity.Property(e => e.ProvinceName).HasMaxLength(128);
            entity.Property(e => e.PostalCode).HasMaxLength(128);
            entity.Property(e => e.CountryName).HasMaxLength(128);
            entity.Property(e => e.LifetimeSpend).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(512).IsUnicode(true);
        });

        modelBuilder.Entity<CustomerPointLog>(entity =>
        {
            entity.HasKey(e => e.CustomerPointLogId).HasName("PK__Customer__EC4A52367E078E40");

            entity.ToTable("CustomerPointLog");

            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerPointLogs)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CustomerP__Custo__3C69FB99");

            entity.HasOne(d => d.Invoice).WithMany(p => p.CustomerPointLogs)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__CustomerP__Invoi__3D5E1FD2");
        });

        modelBuilder.Entity<GiftCard>(entity =>
        {
            entity.HasKey(e => e.GiftCardId).HasName("PK__GiftCard__9FBB0CC15255F7E0");

            entity.ToTable("GiftCard");

            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.FirstValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IssuedDatetime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");
            entity.Property(e => e.SerialNumber).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.GiftCards)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__GiftCard__Custom__4316F928");
        });

        modelBuilder.Entity<GiftCardDetailLog>(entity =>
        {
            entity.HasKey(e => e.GiftCardLogId).HasName("PK__GiftCard__25821EE492C27334");

            entity.ToTable("GiftCardDetailLog");

            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.Decription)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GiftCardUser)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.GiftCard).WithMany(p => p.GiftCardDetailLogs)
                .HasForeignKey(d => d.GiftCardId)
                .HasConstraintName("FK__GiftCardD__GiftC__45F365D3");

            entity.HasOne(d => d.Invoice).WithMany(p => p.GiftCardDetailLogs)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__GiftCardD__Invoi__46E78A0C");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAB54F9FCB44");

            entity.ToTable("Invoice");

            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.ServicesAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Invoice__Custome__32E0915F");

            entity.HasOne(d => d.Manager).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Invoice__Manager__34C8D9D1");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__Invoice__Payment__33D4B598");

            entity.HasOne(d => d.Store).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Invoice__StoreId__31EC6D26");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailId).HasName("PK__InvoiceD__1F157811407C3E2D");

            entity.ToTable("InvoiceDetail");

            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.ServiceFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tip).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__InvoiceDe__Invoi__37A5467C");

            entity.HasOne(d => d.Service).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__InvoiceDe__Servi__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__InvoiceDe__UserI__398D8EEE");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1D380C3CC02");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00A0C27E12C");

            entity.ToTable("Service");

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.VnName).HasMaxLength(200);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__3B82F101E572CB29");

            entity.ToTable("Store");

            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PK__Tip__2DB1A1C8C67787CD");

            entity.ToTable("Tip");

            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsPaidDatetime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Tips)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Tip__UserId__4F7CD00D");
        });

        modelBuilder.Entity<TipLog>(entity =>
        {
            entity.HasKey(e => e.TipLogId).HasName("PK__TipLog__4AE4E3109C1C801A");

            entity.ToTable("TipLog");

            entity.Property(e => e.Action).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.DataName).HasMaxLength(20);
            entity.Property(e => e.DataValue).HasMaxLength(20);

            entity.HasOne(d => d.InvoiceDetail).WithMany(p => p.TipLogs)
                .HasForeignKey(d => d.InvoiceDetailId)
                .HasConstraintName("FK__TipLog__InvoiceD__534D60F1");

            entity.HasOne(d => d.Tip).WithMany(p => p.TipLogs)
                .HasForeignKey(d => d.TipId)
                .HasConstraintName("FK__TipLog__TipId__52593CB8");
        });

        modelBuilder.Entity<TurnLog>(entity =>
        {
            entity.HasKey(e => e.TurnLogId).HasName("PK__TurnLog__6B96DADA80BFE262");

            entity.ToTable("TurnLog");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.TurnLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TurnLog__UserId__4CA06362");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CDB497070");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Address1).HasMaxLength(255);
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.UserGroupId).HasName("PK__UserGrou__FA5A61C0F1AB5C2E");

            entity.ToTable("UserGroup");

            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(20);

            entity.HasOne(d => d.Store).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__UserGroup__Store__286302EC");

            entity.HasOne(d => d.User).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserGroup__UserI__29572725");
        });

        modelBuilder.Entity<WorkDayRecord>(entity =>
        {
            entity.HasKey(e => e.WorkDayRecordId).HasName("PK__WorkDayR__1E82ACBA557E54BC");

            entity.ToTable("WorkDayRecord");

            entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.WorkDayRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__WorkDayRe__UserI__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
