using Microsoft.EntityFrameworkCore;

namespace TourOfHeroesService.Data.Models
{
    public class HeroContext:DbContext
    {
        public HeroContext(DbContextOptions<HeroContext> options):base(options)
        {
        }
        public DbSet<Hero> Heroes { get; set; }
    }
}