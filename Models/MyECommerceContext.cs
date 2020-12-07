using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyECommerce.Models
{
    public partial class MyECommerceContext : DbContext
    {
        public MyECommerceContext()
        {
        }

        public MyECommerceContext(DbContextOptions<MyECommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Business> Business { get; set; }
        public virtual DbSet<Chats> Chats { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Coupons> Coupons { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Evaluates> Evaluates { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ProductGroups> ProductGroups { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Replies> Replies { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=.;Database=MyECommerce;Persist Security Info=True;User ID=sa;Password=!Minh3614534;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__Admins__719FE488E4B4991B");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Admins__A9D105342D0A536B")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasMaxLength(30);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FromAdmin).HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.FromAdminNavigation)
                    .WithMany(p => p.InverseFromAdminNavigation)
                    .HasForeignKey(d => d.FromAdmin)
                    .HasConstraintName("FK__Admins__FromAdmi__398D8EEE");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Admins__RoleId__3A81B327");
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Chats>(entity =>
            {
                entity.HasKey(e => e.ChatId)
                    .HasName("PK__Chats__A9FBE7C6C72AA4A5");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.IsFromShop).HasColumnName("isFromShop");

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Chats__CustomerI__6B24EA82");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Chats__ShopId__6C190EBB");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__Comments__C3B4DFCAAF5486FC");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Comments__Custom__5BE2A6F2");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__Produc__59FA5E80");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Comments__ShopId__5AEE82B9");
            });

            modelBuilder.Entity<Coupons>(entity =>
            {
                entity.HasKey(e => e.CouponId)
                    .HasName("PK__Coupons__384AF1BACF0A4013");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ__Coupons__A25C5AA724385A10")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ShopId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Coupons__ShopId__4316F928");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64D84EE23988");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Customer__A9D105341C366E46")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.AccountNumber).HasMaxLength(20);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customers__RoleI__46E78A0C");
            });

            modelBuilder.Entity<Evaluates>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CustomerId })
                    .HasName("PK__Evaluate__2E462080509FDF5D");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evaluates__Custo__571DF1D5");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evaluates__Produ__5629CD9C");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK__OrderDet__D3B9D36CE42EF8AD");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK__OrderDeta__Coupo__68487DD7");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__66603565");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Produ__6754599E");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BCF3573E6BB");

                entity.Property(e => e.Comment).HasColumnType("text");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TimeOrder).HasColumnType("datetime");

                entity.Property(e => e.TimeRecived).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Customer__6383C8BA");
            });

            modelBuilder.Entity<ProductGroups>(entity =>
            {
                entity.HasKey(e => e.ProductGroupId)
                    .HasName("PK__ProductG__0F0D7B22BB77669D");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.ProductGroups)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductGr__Busin__4F7CD00D");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6CD7089E64C");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Picture).HasColumnType("text");

                entity.Property(e => e.ShopId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.ProductGroup)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Produc__534D60F1");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__ShopId__52593CB8");
            });

            modelBuilder.Entity<Replies>(entity =>
            {
                entity.HasKey(e => e.ReplyId)
                    .HasName("PK__Replies__C25E460933D370F3");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Replies__Comment__5EBF139D");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Replies__Custome__60A75C0F");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Replies__ShopId__5FB337D6");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Shippers>(entity =>
            {
                entity.HasKey(e => e.ShipperId)
                    .HasName("PK__Shippers__1F8AFE596B4A88E5");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Shippers__A9D1053468CB54E7")
                    .IsUnique();

                entity.Property(e => e.ShipperId).HasMaxLength(30);

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.ContractCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TaxCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Shippers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shippers__RoleId__4AB81AF0");
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK__Shops__67C557C9877F0A29");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Shops__A9D10534616AD61F")
                    .IsUnique();

                entity.HasIndex(e => e.ShopName)
                    .HasName("UQ__Shops__649A7D9636908228")
                    .IsUnique();

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.ContractCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shops__RoleId__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
