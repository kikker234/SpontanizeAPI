using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class SpontanizeContext : IdentityDbContext<User>
{
    public DbSet<Deal> Deals { get; set; }
    public DbSet<Organization> Organization { get; set; }

    public SpontanizeContext(DbContextOptions<SpontanizeContext> options) : base(options)
    {
    }
}