using crud_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_webapi.Data;

public class UserApiDbContext : DbContext
{
    public UserApiDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
