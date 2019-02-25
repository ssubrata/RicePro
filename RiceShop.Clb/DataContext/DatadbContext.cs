using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiceShop.Clb.Entity;

namespace RiceShop.Clb
{
    public  class DatadbContext:IdentityDbContext<ApplicationUser, AppIdentityRole, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Due> Dues { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        //Parameter less Constructor for Reposiotry;
        public DatadbContext() {}

        //Parameter  Constructor for Initial Identity Core;
        public DatadbContext(DbContextOptions<DatadbContext> options)
            : base(options){ }

      //  Connection for Parameter less Constructor
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"server=DESKTOP-H9MKKNS\SQLEXPRESS;database=RichShop;trusted_connection=true;");
            }
        }

        //this is for fluent api to model ;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }

    
}
