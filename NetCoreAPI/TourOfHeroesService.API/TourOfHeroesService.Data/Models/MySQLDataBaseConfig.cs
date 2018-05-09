using Microsoft.EntityFrameworkCore;

namespace TourOfHeroesService.Data.Models
{
    public class MySqlDataBaseConfig
    {
        private const string DefaultMySqlConnectionString = "server=localhost;userid=root;pwd=090420;port=3306;database=heroes;";
        public static HeroContext CreateContext(string mySqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(mySqlConnectionString))
            {
                mySqlConnectionString = DefaultMySqlConnectionString;
            }
            var optionBuilder = new DbContextOptionsBuilder<HeroContext>();
            optionBuilder.UseMySQL(mySqlConnectionString);
            var context = new HeroContext(optionBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}