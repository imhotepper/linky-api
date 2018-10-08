
using Microsoft.EntityFrameworkCore;

public class AppDb: DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<Show> Shows { get; set; }
     public AppDb(DbContextOptions<AppDb> options) : base(options)
    {
    }
}