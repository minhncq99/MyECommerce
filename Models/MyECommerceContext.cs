﻿using System;
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
        public virtual DbSet<ChatDetails> ChatDetails { get; set; }
        public virtual DbSet<Chats> Chats { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Coupons> Coupons { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Deliveries> Deliveries { get; set; }
        public virtual DbSet<Evaluates> Evaluates { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ProductGroups> ProductGroups { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Replies> Replies { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<Warehose> Warehose { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyECommerce;Persist Security Info=True;User ID=sa;Password=123123;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__Admins__719FE488BC9C3081");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Admins__A9D10534015A3A1D")
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

            modelBuilder.Entity<ChatDetails>(entity =>
            {
                entity.HasKey(e => e.ChatDetailId)
                    .HasName("PK__ChatDeta__DA92694CA7283B3E");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatDetails)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatDetai__ChatI__656C112C");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ChatDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__ChatDetai__Custo__6383C8BA");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.ChatDetails)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__ChatDetai__ShopI__6477ECF3");
            });

            modelBuilder.Entity<Chats>(entity =>
            {
                entity.HasKey(e => e.ChatId)
                    .HasName("PK__Chats__A9FBE7C63B6B5B50");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Chats__CustomerI__5FB337D6");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Chats__ShopId__60A75C0F");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__Comments__C3B4DFCAFE2F724E");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Comments__Custom__48CFD27E");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__Produc__46E78A0C");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Comments__ShopId__47DBAE45");
            });

            modelBuilder.Entity<Coupons>(entity =>
            {
                entity.HasKey(e => e.CouponId)
                    .HasName("PK__Coupons__384AF1BA92662C36");

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
                    .HasConstraintName("FK__Coupons__ShopId__300424B4");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64D80E30F31B");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Customer__A9D1053436D32C80")
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
                    .HasConstraintName("FK__Customers__RoleI__33D4B598");
            });

            modelBuilder.Entity<Deliveries>(entity =>
            {
                entity.HasKey(e => e.DeliveryId)
                    .HasName("PK__Deliveri__626D8FCE31E5DC86");

                entity.Property(e => e.ShipperId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Deliverie__Order__5BE2A6F2");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.ShipperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Deliverie__Shipp__5CD6CB2B");
            });

            modelBuilder.Entity<Evaluates>(entity =>
            {
                entity.HasKey(e => e.EvaluateId)
                    .HasName("PK__Evaluate__2092E4BA9D2D4445");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evaluates__Custo__440B1D61");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evaluates__Produ__4316F928");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("PK__Notifica__20CF2E12646A5447");

                entity.Property(e => e.AdminId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__Admin__68487DD7");

                entity.HasOne(d => d.TypeUser)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TypeUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__TypeU__693CA210");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK__OrderDet__D3B9D36C81B8504E");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK__OrderDeta__Coupo__5535A963");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__534D60F1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Produ__5441852A");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BCF704FC031");

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
                    .HasConstraintName("FK__Orders__Customer__5070F446");
            });

            modelBuilder.Entity<ProductGroups>(entity =>
            {
                entity.HasKey(e => e.ProductGroupId)
                    .HasName("PK__ProductG__0F0D7B2201C15536");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.ProductGroups)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductGr__Busin__3C69FB99");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6CDDBC0FDD1");

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
                    .HasConstraintName("FK__Products__Produc__403A8C7D");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__ShopId__3F466844");
            });

            modelBuilder.Entity<Replies>(entity =>
            {
                entity.HasKey(e => e.ReplyId)
                    .HasName("PK__Replies__C25E4609C3B14801");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(30);

                entity.Property(e => e.ShopId).HasMaxLength(30);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Replies__Comment__4BAC3F29");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Replies__Custome__4D94879B");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Replies__ShopId__4CA06362");
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
                    .HasName("PK__Shippers__1F8AFE59F84ECB15");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Shippers__A9D1053488E549FB")
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
                    .HasConstraintName("FK__Shippers__RoleId__37A5467C");
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK__Shops__67C557C9F9EED04A");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Shops__A9D1053462C1B61E")
                    .IsUnique();

                entity.HasIndex(e => e.ShopName)
                    .HasName("UQ__Shops__649A7D96E67CA3FD")
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

            modelBuilder.Entity<Warehose>(entity =>
            {
                entity.Property(e => e.ShipperId)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.Warehose)
                    .HasForeignKey(d => d.OrderDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Warehose__OrderD__5812160E");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Warehose)
                    .HasForeignKey(d => d.ShipperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Warehose__Shippe__59063A47");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}