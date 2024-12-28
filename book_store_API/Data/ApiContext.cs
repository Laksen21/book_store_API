using Microsoft.EntityFrameworkCore;
using book_store_API.Models;

namespace book_store_API.Data
{
    public class ApiContext : DbContext 
    {
        public DbSet<Book> Books { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) 
        {

        }
    }
}
