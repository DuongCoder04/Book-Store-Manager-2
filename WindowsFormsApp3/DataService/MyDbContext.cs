using System.Data.Entity;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.DataService
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=MyDbContext") // Thay đổi tên chuỗi kết nối cho phù hợp
        {
            var initializer = new MigrateDatabaseToLatestVersion<MyDbContext, Migrations.Configuration>();
            Database.SetInitializer(initializer);
        }
        public virtual DbSet<Book> myBooks { get; set; }
        public virtual DbSet<Category> myCategory { get; set; }
        public virtual DbSet<Authors> myAuthors { get; set; }
        public virtual DbSet<Publisher> myPublisher { get; set; }
        public virtual DbSet<Staff> myStaff { get; set; }
        public virtual DbSet<User> myUser { get; set; }
        public virtual DbSet<Bill> myBill { get; set; }
        public virtual DbSet<BillDetail> myBillDetail { get; set; }
        public virtual DbSet<Customer> myCustomer { get; set; }

    }
}
// Các lệnh migrations (chạy trong Package Manager Console)
// enable-migrations -EnableAutomaticMigration:$true
// Add-Migration Initial -IgnoreChanges
// Update-Database -verbose