using System;
using System.Collections.Generic;
using TourOfHeroesService.Services.Common;
using TourOfHeroesService.Services.HeroManagers.ViewModels;

namespace TourOfHeroesService.Services.HeroManagers
{
    public interface IHeroManager: IManagerBase
    {
        bool InitDataBase();

        HeroViewModel GetHero(int id);
        IEnumerable<HeroViewModel> GetHeroes();
        bool AddHero(HeroViewModel model);
        bool DeleteHero(int id);
    }
}