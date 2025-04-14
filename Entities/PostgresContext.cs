using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace upp.Entities;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Finishedproduct> Finishedproducts { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Production> Productions { get; set; }

    public virtual DbSet<Productionreport> Productionreports { get; set; }

    public virtual DbSet<QualityControl> QualityControls { get; set; }

    public virtual DbSet<Rawmaterial> Rawmaterials { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<StorageReport> StorageReports { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5432;database=postgres;username=postgres;password=123123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("customer_orders_pkey");

            entity.ToTable("customer_orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(100)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .HasColumnName("contact_phone");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("customer_name");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("department");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
        });

        modelBuilder.Entity<Finishedproduct>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("finishedproducts_pkey");

            entity.ToTable("finishedproducts");

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Productname)
                .HasMaxLength(100)
                .HasColumnName("productname");
            entity.Property(e => e.Quantity)
                .HasPrecision(10, 2)
                .HasColumnName("quantity");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("order_items_pkey");

            entity.ToTable("order_items");

            entity.HasIndex(e => new { e.OrderId, e.ProductId }, "unique_order_product").IsUnique();

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(10, 2)
                .HasColumnName("quantity");
            entity.Property(e => e.ShipmentId).HasColumnName("shipment_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_product_id_fkey");

            entity.HasOne(d => d.Shipment).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ShipmentId)
                .HasConstraintName("order_items_shipment_id_fkey");
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.HasKey(e => e.Productionid).HasName("production_pkey");

            entity.ToTable("production");

            entity.Property(e => e.Productionid).HasColumnName("productionid");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Productiondate).HasColumnName("productiondate");
            entity.Property(e => e.Quantityproduced)
                .HasPrecision(10, 2)
                .HasColumnName("quantityproduced");

            entity.HasOne(d => d.Employee).WithMany(p => p.Productions)
                .HasForeignKey(d => d.Employeeid)
                .HasConstraintName("production_employeeid_fkey");

            entity.HasOne(d => d.Material).WithMany(p => p.Productions)
                .HasForeignKey(d => d.Materialid)
                .HasConstraintName("production_materialid_fkey");
        });

        modelBuilder.Entity<Productionreport>(entity =>
        {
            entity.HasKey(e => e.Productionreportid).HasName("productionreports_pkey");

            entity.ToTable("productionreports");

            entity.Property(e => e.Productionreportid).HasColumnName("productionreportid");
            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Quantityproduced)
                .HasPrecision(10, 2)
                .HasColumnName("quantityproduced");
            entity.Property(e => e.Quantityused)
                .HasPrecision(10, 2)
                .HasColumnName("quantityused");
            entity.Property(e => e.Reportdate).HasColumnName("reportdate");

            entity.HasOne(d => d.Material).WithMany(p => p.Productionreports)
                .HasForeignKey(d => d.Materialid)
                .HasConstraintName("productionreports_materialid_fkey");
        });

        modelBuilder.Entity<QualityControl>(entity =>
        {
            entity.HasKey(e => e.ControlId).HasName("quality_control_pkey");

            entity.ToTable("quality_control");

            entity.Property(e => e.ControlId).HasColumnName("control_id");
            entity.Property(e => e.CheckDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("check_date");
            entity.Property(e => e.DefectDescription).HasColumnName("defect_description");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductionId).HasColumnName("production_id");
            entity.Property(e => e.QuantityChecked)
                .HasPrecision(10, 2)
                .HasColumnName("quantity_checked");
            entity.Property(e => e.QuantityRejected)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("quantity_rejected");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.QualityControls)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("quality_control_employee_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.QualityControls)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("quality_control_product_id_fkey");

            entity.HasOne(d => d.Production).WithMany(p => p.QualityControls)
                .HasForeignKey(d => d.ProductionId)
                .HasConstraintName("quality_control_production_id_fkey");
        });

        modelBuilder.Entity<Rawmaterial>(entity =>
        {
            entity.HasKey(e => e.Materialid).HasName("rawmaterials_pkey");

            entity.ToTable("rawmaterials");

            entity.Property(e => e.Materialid).HasColumnName("materialid");
            entity.Property(e => e.Materialname)
                .HasMaxLength(100)
                .HasColumnName("materialname");
            entity.Property(e => e.Quantity)
                .HasPrecision(10, 2)
                .HasColumnName("quantity");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Shipmentid).HasName("shipments_pkey");

            entity.ToTable("shipments");

            entity.Property(e => e.Shipmentid).HasColumnName("shipmentid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantityshipped)
                .HasPrecision(10, 2)
                .HasColumnName("quantityshipped");
            entity.Property(e => e.Shipmentdate).HasColumnName("shipmentdate");

            entity.HasOne(d => d.Product).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("shipments_productid_fkey");
        });

        modelBuilder.Entity<StorageReport>(entity =>
        {
            entity.HasKey(e => e.Reportid).HasName("storage_reports_pkey");

            entity.ToTable("storage_reports");

            entity.Property(e => e.Reportid).HasColumnName("reportid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantityinstock)
                .HasPrecision(10, 2)
                .HasColumnName("quantityinstock");
            entity.Property(e => e.Reportdate).HasColumnName("reportdate");

            entity.HasOne(d => d.Product).WithMany(p => p.StorageReports)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("storage_reports_productid_fkey");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Warehouseid).HasName("warehouse_pkey");

            entity.ToTable("warehouse");

            entity.Property(e => e.Warehouseid).HasColumnName("warehouseid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantitystored)
                .HasPrecision(10, 2)
                .HasColumnName("quantitystored");
            entity.Property(e => e.Storagedate).HasColumnName("storagedate");

            entity.HasOne(d => d.Product).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("warehouse_productid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
