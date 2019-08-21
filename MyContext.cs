using Microsoft.EntityFrameworkCore;

namespace bankaccount.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> User {get;set;}
        public DbSet<Transaction> Transaction{get;set;}
    }
}
