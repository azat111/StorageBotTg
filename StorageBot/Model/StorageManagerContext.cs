using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StorageBot.Model
{
    public partial class StorageManagerContext : DbContext
    {
        public StorageManagerContext()
        {
        }

        public StorageManagerContext(DbContextOptions<StorageManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catering> Catering { get; set; }
        public virtual DbSet<CateringDetails> CateringDetails { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<ExpenseForms> ExpenseForms { get; set; }
        public virtual DbSet<ExpenseProducts> ExpenseProducts { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersProduct> OrdersProduct { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<StatusTypes> StatusTypes { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseProduct> WarehouseProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0QF8C26;Database=StorageManager;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catering>(entity =>
            {
                entity.ToTable("catering");

                entity.Property(e => e.CateringId).HasColumnName("catering_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.EventAddress)
                    .HasColumnName("event_address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventDate)
                    .HasColumnName("event_date")
                    .HasColumnType("date");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("date");

                entity.Property(e => e.TotalCost).HasColumnName("total_cost");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Catering)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__catering__custom__6383C8BA");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Catering)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_catering_status_types");
            });

            modelBuilder.Entity<CateringDetails>(entity =>
            {
                entity.HasKey(e => new { e.CateringId, e.ProductId })
                    .HasName("PK__catering__6189425D41ABEFB3");

                entity.ToTable("catering_details");

                entity.Property(e => e.CateringId).HasColumnName("catering_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Catering)
                    .WithMany(p => p.CateringDetails)
                    .HasForeignKey(d => d.CateringId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__catering___cater__66603565");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CateringDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__catering___produ__6754599E");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB852A8582C8");

                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customer_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExpenseForms>(entity =>
            {
                entity.HasKey(e => e.ExpenseId)
                    .HasName("PK__expense___404B6A6BB77BF34B");

                entity.ToTable("expense_forms");

                entity.Property(e => e.ExpenseId).HasColumnName("expense_id");

                entity.Property(e => e.ExpenseDate)
                    .HasColumnName("expense_date")
                    .HasColumnType("date");

                entity.Property(e => e.TotalCost).HasColumnName("total_cost");
            });

            modelBuilder.Entity<ExpenseProducts>(entity =>
            {
                entity.HasKey(e => new { e.ExpenseId, e.ProductId })
                    .HasName("pk_ExpenseProducts");

                entity.Property(e => e.ExpenseId).HasColumnName("expense_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Expense)
                    .WithMany(p => p.ExpenseProducts)
                    .HasForeignKey(d => d.ExpenseId)
                    .HasConstraintName("FK_ExpenseProducts_expense_forms");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ExpenseProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ExpenseProducts_products");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("PK__invoices__F58DFD4938F61975");

                entity.ToTable("invoices");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnName("invoice_date")
                    .HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasColumnName("invoice_number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.TotalCost)
                    .HasColumnName("total_cost")
                    .HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__invoices__suppli__5DCAEF64");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__orders__465962290409C27D");

                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.OrderCost).HasColumnName("order_cost");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_orders_customers");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_orders_status_types");
            });

            modelBuilder.Entity<OrdersProduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("pk_OrdersProduct");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersProduct)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrdersProduct_orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrdersProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersProduct_products");
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__product___D54EE9B4E54422DE");

                entity.ToTable("product_categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductTypes>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__product___2C0005985C906C27");

                entity.ToTable("product_types");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.TypeName)
                    .HasColumnName("type_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__products__47027DF575388A75");

                entity.ToTable("products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Manufacturer)
                    .HasColumnName("manufacturer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__products__catego__5441852A");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__products__type_i__5535A963");
            });

            modelBuilder.Entity<StatusTypes>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.ToTable("status_types");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.StatusName)
                    .HasColumnName("status_name")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK__supplier__6EE594E86CAE3FA9");

                entity.ToTable("suppliers");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierName)
                    .HasColumnName("supplier_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__users__B9BE370F36D2DC56");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("warehouse");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<WarehouseProduct>(entity =>
            {
                entity.HasKey(e => new { e.WarehouseId, e.ProductId })
                    .HasName("pk_WarehouseProduct");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseProduct_products");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseProduct)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_WarehouseProduct_warehouse");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
