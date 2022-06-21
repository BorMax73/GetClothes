using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetClothes.Data;
using GetClothes.Models;
using GetClothes.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GetClothes.Controllers
{
    public class ClothesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;

        public ClothesController(ApplicationContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string query)
        {
            List<Article> articles;
            if (string.IsNullOrEmpty(query))
            {
                articles = await _db.Articles.ToListAsync();
            }

            articles = await _db.Articles.Where(x => EF.Functions.Like(x.Title, $"%{query}%")).ToListAsync();
            List<ArticleViewModel> vm = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                var description = await _db.ArticleDescriptions.FirstOrDefaultAsync(x => x.ArticleId == article.Id);
                var image = await _db.ArticleImages.FirstOrDefaultAsync(x => x.ArticleId == article.Id);

                ArticleViewModel articleViewModel = new ArticleViewModel()
                {
                    Id = article.Id,
                    Size = description.Size,
                    Title = article.Title,
                    Color = description.Color,
                    Price = article.Price,
                    Other = description.Other,
                    Image = image.Image
                };
                vm.Add(articleViewModel);
            }
            return View(vm);
        }
    }
}
