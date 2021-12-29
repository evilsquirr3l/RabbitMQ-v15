using Microsoft.EntityFrameworkCore;
using Subscriber.Models;

namespace Subscriber;

public class CardsDbContext : DbContext
{
    public CardsDbContext(DbContextOptions<CardsDbContext> options) : base(options)
    {
        
    }

    public DbSet<Card> Cards { get; set; }
}