using System.Collections.Generic;
using System.Linq;
using TourOfHeroesService.Data.Models;
using TourOfHeroesService.Services.Common;
using TourOfHeroesService.Services.HeroManagers.ViewModels;

namespace TourOfHeroesService.Services.HeroManagers
{
    public class HeroManager : ManagerBase, IHeroManager
    {
        public HeroViewModel GetHero(int id)
        {
            var hero = Uow.Repository<Hero>().Query(h => h.Id == id).Select(h => new HeroViewModel { Id = h.Id, Name = h.Name }).FirstOrDefault();
            return hero;
        }

        public IEnumerable<HeroViewModel> GetHeroes()
        {
            var heroes = Uow.Repository<Hero>().Query().Select(h => new HeroViewModel { Id = h.Id, Name = h.Name }).ToList();
            return heroes;
        }

        public bool AddHero(HeroViewModel model)
        {
            var hero = new Hero
            {
                Name = model.Name
            };
            Uow.Repository<Hero>().Add(hero);
            Uow.Commit();
            return true;
        }

        public bool DeleteHero(int id)
        {
            var hero = Uow.Repository<Hero>().Query(h => h.Id == id).FirstOrDefault();
            Uow.Repository<Hero>().Delete(hero);
            Uow.Commit();
            return true;
        }

        public bool InitDataBase()
        {
            using (HeroContext context = MySqlDataBaseConfig.CreateContext())
            {
                context.Heroes.Add(new Hero { Name = "Aaron" });
                return context.SaveChanges() > 0;
            }
        }
    }
}