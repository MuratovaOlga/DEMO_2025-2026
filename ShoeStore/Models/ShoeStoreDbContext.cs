using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoeStore.Models;

public partial class ShoeStoreDbContext : DbContext
{
    public ShoeStoreDbContext()
    {
    }

    public ShoeStoreDbContext(DbContextOptions<ShoeStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<OrdersProduct> OrdersProducts { get; set; }

    public virtual DbSet<PickUpPpoint> PickUpPpoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductsByOrder> ProductsByOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatusOrder> StatusOrders { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MURATOVAOV\\SQLEXPRESS;Initial Catalog=ShoeStoreDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManufacturer).HasName("PK__Manufact__8A22D026ADF5D452");

            entity.Property(e => e.IdManufacturer).HasColumnName("Id_Manufacturer");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<OrdersProduct>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__OrdersPr__370733B2136D2289");

            entity.ToTable("OrdersProduct");

            entity.Property(e => e.IdOrder).HasColumnName("Id_Order");
            entity.Property(e => e.DeliveryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdPickUpPpoint).HasColumnName("Id_PickUpPpoint");
            entity.Property(e => e.IdStatus).HasColumnName("Id_Status");
            entity.Property(e => e.IdUser).HasColumnName("Id_User");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdPickUpPpointNavigation).WithMany(p => p.OrdersProducts)
                .HasForeignKey(d => d.IdPickUpPpoint)
                .HasConstraintName("FK__OrdersPro__Id_Pi__4D94879B");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.OrdersProducts)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK__OrdersPro__Id_St__4F7CD00D");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.OrdersProducts)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__OrdersPro__Id_Us__4E88ABD4");
        });

        modelBuilder.Entity<PickUpPpoint>(entity =>
        {
            entity.HasKey(e => e.IdPickUpPpoint).HasName("PK__PickUpPp__7190554726BD346E");

            entity.Property(e => e.IdPickUpPpoint).HasColumnName("Id_PickUpPpoint");
            entity.Property(e => e.AddressPickUpPpoint).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Products__6B19B3E72696DE7E");

            entity.Property(e => e.IdProduct).HasColumnName("Id_Product");
            entity.Property(e => e.ArticleProduct).HasMaxLength(20);
            entity.Property(e => e.IdManufacturer).HasColumnName("Id_Manufacturer");
            entity.Property(e => e.IdProductCategory).HasColumnName("Id_ProductCategory");
            entity.Property(e => e.IdSupplier).HasColumnName("Id_Supplier");
            entity.Property(e => e.IdWarehouse).HasColumnName("Id_Warehouse");
            entity.Property(e => e.NameProduct).HasMaxLength(100);
            entity.Property(e => e.PhotoProduct).HasMaxLength(255);
            entity.Property(e => e.PriceProduct).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitOfMeasurementProduct).HasMaxLength(10);

            entity.HasOne(d => d.IdManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdManufacturer)
                .HasConstraintName("FK__Products__Id_Man__440B1D61");

            entity.HasOne(d => d.IdProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProductCategory)
                .HasConstraintName("FK__Products__Id_Pro__44FF419A");

            entity.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdSupplier)
                .HasConstraintName("FK__Products__Id_Sup__4316F928");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdWarehouse)
                .HasConstraintName("FK__Products__Id_War__45F365D3");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.IdProductCategory).HasName("PK__ProductC__E29CB9AEA8E0D5C2");

            entity.Property(e => e.IdProductCategory).HasColumnName("Id_ProductCategory");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductsByOrder>(entity =>
        {
            entity.HasKey(e => e.IdProductsByOrder).HasName("PK__Products__AC67E139821C6A38");

            entity.Property(e => e.IdProductsByOrder).HasColumnName("Id_ProductsByOrder");
            entity.Property(e => e.IdOrder).HasColumnName("Id_Order");
            entity.Property(e => e.IdProduct).HasColumnName("Id_Product");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.ProductsByOrders)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__ProductsB__Id_Or__534D60F1");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductsByOrders)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__ProductsB__Id_Pr__52593CB8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__34ADFA60EC1D8955");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("Id_Role");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<StatusOrder>(entity =>
        {
            entity.HasKey(e => e.IdStatusOrder).HasName("PK__StatusOr__D80D8437956FB128");

            entity.ToTable("StatusOrder");

            entity.Property(e => e.IdStatusOrder).HasColumnName("Id_StatusOrder");
            entity.Property(e => e.StatusOrderName).HasMaxLength(100);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.IdSupplier).HasName("PK__Supplier__BDD7A8AEB2F80768");

            entity.Property(e => e.IdSupplier).HasColumnName("Id_Supplier");
            entity.Property(e => e.NameSupplier).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__D03DEDCBAD12AA9B");

            entity.Property(e => e.IdUser).HasColumnName("Id_User");
            entity.Property(e => e.Fiouser)
                .HasMaxLength(100)
                .HasColumnName("FIOUser");
            entity.Property(e => e.IdRole).HasColumnName("Id_Role");
            entity.Property(e => e.LoginUser).HasMaxLength(100);
            entity.Property(e => e.PasswordUser).HasMaxLength(100);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK__Users__Id_Role__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
