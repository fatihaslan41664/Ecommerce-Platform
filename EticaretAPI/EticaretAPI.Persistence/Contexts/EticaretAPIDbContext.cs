using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Contexts
{
    public class EticaretAPIDbContext : IdentityDbContext<AppUser,AppRole, string>
    {
        public EticaretAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MyFile> MyFiles { get; set; }
        public DbSet<ProductImageFile> ProductImages { get; set; }
        public DbSet<InvoiceFile> ınvoiceFiles { get; set; }
        public DbSet<BasketItems> BasketItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<EndPoint> EndPoints { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasKey(b => b.Id);
            builder.Entity<Order>()
                .HasIndex(o=>o.OrderCode)
                .IsUnique();
            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
            builder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithMany(pif => pif.Product)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductProductImageFile",
                    j => j.HasOne<ProductImageFile>().WithMany().HasForeignKey("ProductImageFileId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId").OnDelete(DeleteBehavior.Cascade)
                );
            builder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(x => x.OrderId);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                    data.Entity.CreatedDate = DateTime.UtcNow;
                else if (data.State == EntityState.Modified)
                    data.Entity.UpdateDate = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
