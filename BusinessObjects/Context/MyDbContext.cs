using System;
using System.Collections.Generic;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(GetConnectionString());


    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:Deploy"];
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B2F297C4E");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6D965CFB55C2");

            entity.ToTable("Discount");

            entity.Property(e => e.CreatedDate).HasColumnType("timestamp");
            entity.Property(e => e.DiscountCode).HasMaxLength(10);
            entity.Property(e => e.DiscountName).HasMaxLength(10);
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.ExpiredDate).HasColumnType("timestamp");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF78BAC86D");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ConfirmDate).HasColumnType("timestamp");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.OrderDate).HasColumnType("timestamp");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Discount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_Orders_Discount");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36CCD289F82");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Order_OrderDetail");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD1C56FD70");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.ProductDetailId).HasName("PK__ProductD__3C8DD6B409D24B48");

            entity.ToTable("ProductDetail");

            entity.Property(e => e.ProductParam).HasMaxLength(255);
            entity.Property(e => e.ProductValue).HasMaxLength(255);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79CECC0CC933");

            entity.ToTable("Review");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Review_Product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AFF7616EC");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("PK__Shipping__5FACD580F1AD9233");

            entity.ToTable("Shipping");

            entity.HasIndex(e => e.OrderId, "UQ_Shipping_OrderId").IsUnique();

            entity.Property(e => e.ShippingId).ValueGeneratedNever();
            entity.Property(e => e.CancelDate).HasColumnType("timestamp");
            entity.Property(e => e.EndShipDate).HasColumnType("timestamp");
            entity.Property(e => e.ShippingAddress).HasMaxLength(255);
            entity.Property(e => e.StartShipDate).HasColumnType("timestamp");
            entity.Property(e => e.TrackingNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C740CFE98");

            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("timestamp");
            entity.Property(e => e.Email)
                .HasMaxLength(125)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(125);
            entity.Property(e => e.Image)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(125);
            entity.Property(e => e.Username).HasMaxLength(125);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
