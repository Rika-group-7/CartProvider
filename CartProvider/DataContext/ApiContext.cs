using CartProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace CartProvider.DataContext;

public class ApiContext: DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

    public DbSet<CartItem> DbCartItems { get; set; }
}
