using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Configuration.Json;

namespace HastBuyDAL.Models;

public partial class HastBuyDbContext : DbContext
{
    public HastBuyDbContext()
    {
    }

    public HastBuyDbContext(DbContextOptions<HastBuyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<InventoryLog> InventoryLogs { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PromoCode> PromoCodes { get; set; }

    public virtual DbSet<PromoUsage> PromoUsages { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build().GetConnectionString("HustBuyDBConnectionString"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1B6A397F6C");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Street).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Addresses__UserI__2A4B4B5E");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5499A843148173");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.ActionTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Module).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B2A757B42E4");

            entity.Property(e => e.CartItemId).HasColumnName("CartItemID");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__CartItems__Produ__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CartItems__UserI__3F466844");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BB4CA5197");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK__Categorie__Paren__31EC6D26");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PK__Deliveri__626D8FEE3367570F");

            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveredAt).HasColumnType("datetime");
            entity.Property(e => e.DeliveryAgentId).HasColumnName("DeliveryAgentID");
            entity.Property(e => e.DeliveryStatus).HasMaxLength(50);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.DeliveryAgent).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.DeliveryAgentId)
                .HasConstraintName("FK__Deliverie__Deliv__5CD6CB2B");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Deliverie__Order__5BE2A6F2");
        });

        modelBuilder.Entity<InventoryLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Inventor__5E5499A8E1E519ED");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ChangeType).HasMaxLength(50);
            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");

            entity.HasOne(d => d.Product).WithMany(p => p.InventoryLogs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Inventory__Produ__60A75C0F");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32A316C373");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__6754599E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFCFF8E2C0");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Orders__AddressI__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__440B1D61");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A1F7FB7E44");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__48CFD27E");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderItem__Produ__49C3F6B7");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A589F239803");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payments__OrderI__4CA06362");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED17043D6E");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PromoCode>(entity =>
        {
            entity.HasKey(e => e.PromoId).HasName("PK__PromoCod__33D334D00B109E66");

            entity.Property(e => e.PromoId).HasColumnName("PromoID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.UsageCount).HasDefaultValue(0);
        });

        modelBuilder.Entity<PromoUsage>(entity =>
        {
            entity.HasKey(e => e.UsageId).HasName("PK__PromoUsa__29B197C03E75E750");

            entity.ToTable("PromoUsage");

            entity.Property(e => e.UsageId).HasColumnName("UsageID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PromoId).HasColumnName("PromoID");
            entity.Property(e => e.UsedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Order).WithMany(p => p.PromoUsages)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__PromoUsag__Order__59063A47");

            entity.HasOne(d => d.Promo).WithMany(p => p.PromoUsages)
                .HasForeignKey(d => d.PromoId)
                .HasConstraintName("FK__PromoUsag__Promo__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.PromoUsages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PromoUsag__UserI__571DF1D5");
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__Returns__F445E988B0F980B7");

            entity.Property(e => e.ReturnId).HasColumnName("ReturnID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.RefundAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Returns)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Returns__OrderID__4F7CD00D");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE2906D6AB");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Reviews__Product__34C8D9D1");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__35BCFE0A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACA81F8839");

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359E2DF62AB9").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534084125CA").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("PK__Wishlist__233189CB57CA50E4");

            entity.ToTable("Wishlist");

            entity.Property(e => e.WishlistId).HasColumnName("WishlistID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Wishlist__Produc__3B75D760");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Wishlist__UserID__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
