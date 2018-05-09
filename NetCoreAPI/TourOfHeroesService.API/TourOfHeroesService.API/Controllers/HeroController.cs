using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroesService.Services.HeroManagers;
using TourOfHeroesService.Services.HeroManagers.ViewModels;

namespace TourOfHeroesService.API.Controllers
{
    [Route("api/[controller]")]
    public class HeroController : Controller
    {
    public IHeroManager HeroManager { get; set; }

        public HeroController(IHeroManager heroManager)
        {
            HeroManager = heroManager;
        }
       
        // GET
        public string Index()
        {
            return "Server started.";
        }

        [HttpGet]
        [Route("Init")]
        public bool InitDataBase()
        {
            var result = HeroManager.InitDataBase();
            return result;
        }
        
        [HttpGet("GetHeroes")]
        public IEnumerable<HeroViewModel> GetHeroes()
        {
            var heroes = HeroManager.GetHeroes();
            return heroes;
        }
        
        [HttpGet("GetHero/{id:int}")]
        public HeroViewModel GetHero(int id)
        {
            return HeroManager.GetHero(id);
        }
        
        [HttpPost("AddHero")]
        public bool AddHero(HeroViewModel heroViewModel)
        {
            return HeroManager.AddHero(heroViewModel);
        }

        [HttpGet("DeleteHero/{id}")]
        public bool DeleteHero(int id)
        {
            return HeroManager.DeleteHero(id);
        }
    }
}