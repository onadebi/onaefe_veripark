using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryManagement.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer(new ApplicationDbContextInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BorrowHistory> BorrowHistories { get; set; }

        public DbSet<BusinessHoliday> BusinessHolidays { get; set; }
    }
}