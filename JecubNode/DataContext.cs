using Microsoft.EntityFrameworkCore;

namespace JecubNode
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Todo> Task => Set<Todo>();
    }

}