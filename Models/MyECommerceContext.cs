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
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__Admins__719FE488C5E3D33F");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Admins__A9D1053485B1E26B")
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
                    .HasConstraintName("FK__Admins__FromAdmi__276EDEB3");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Admins__RoleId__286302EC");
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
                    .HasName("PK__Chats__A9FBE7C644518764");

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
                    .HasConstraintName("FK__Chats__CustomerI__59063A47");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Chats__ShopId__59FA5E80");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__Comments__C3B4DFCA18FA7ED8");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Comments__Custom__49C3F6B7");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__Produc__47DBAE45");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Comments__ShopId__48CFD27E");
            });

            modelBuilder.Entity<Coupons>(entity =>
            {
                entity.HasKey(e => e.CouponId)
                    .HasName("PK__Coupons__384AF1BA2A3E80E2");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ__Coupons__A25C5AA73DB49CB7")
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
                    .HasConstraintName("FK__Coupons__ShopId__30F848ED");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64D834055A4C");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Customer__A9D10534BECE4D00")
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
                    .HasConstraintName("FK__Customers__RoleI__34C8D9D1");
            });

            modelBuilder.Entity<Evaluates>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CustomerId })
                    .HasName("PK__Evaluate__2E462080292EB846");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evaluates__Custo__44FF419A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evaluates__Produ__440B1D61");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK__OrderDet__D3B9D36C84426E81");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK__OrderDeta__Coupo__5629CD9C");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__5441852A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Produ__5535A963");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BCFBBFF902B");

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
                    .HasConstraintName("FK__Orders__Customer__5165187F");
            });

            modelBuilder.Entity<ProductGroups>(entity =>
            {
                entity.HasKey(e => e.ProductGroupId)
                    .HasName("PK__ProductG__0F0D7B2226D44180");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.ProductGroups)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductGr__Busin__3D5E1FD2");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6CD5FD78822");

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
                    .HasConstraintName("FK__Products__Produc__412EB0B6");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__ShopId__403A8C7D");
            });

            modelBuilder.Entity<Replies>(entity =>
            {
                entity.HasKey(e => e.ReplyId)
                    .HasName("PK__Replies__C25E460998CBC502");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Replies__Comment__4CA06362");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Replies__Custome__4E88ABD4");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Replies__ShopId__4D94879B");
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
                    .HasName("PK__Shippers__1F8AFE5903C7D687");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Shippers__A9D10534F9A38207")
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
                    .HasConstraintName("FK__Shippers__RoleId__38996AB5");
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK__Shops__67C557C9C20BECDD");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Shops__A9D10534796F32A8")
                    .IsUnique();

                entity.HasIndex(e => e.ShopName)
                    .HasName("UQ__Shops__649A7D9625375BB1")
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
                    .HasConstraintName("FK__Shops__RoleId__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
