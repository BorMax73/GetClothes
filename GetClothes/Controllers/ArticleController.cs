using System.Diagnostics;
using System.IO;
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
    public class ArticleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;

        public ArticleController(ApplicationContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int id)
        {
            var article = await _db.Articles.FirstOrDefaultAsync(x => x.Id == id);
            if (article == null)
                return NotFound();
            var articleDescription = await _db.ArticleDescriptions.FirstOrDefaultAsync(x => x.ArticleId == id);
            var articleImages = await _db.ArticleImages.Where(x => x.ArticleId == id).ToListAsync();

            ViewBag.Article = article;
            ViewBag.ArticleDescription = articleDescription;
            ViewBag.Images = articleImages;

            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        [HttpGet]
        public  IActionResult Create(string key)
        {
            if (key!= "EZcDH47NP2CcDJMN")
            {
                return BadRequest();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel articleVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Article article = new Article() {Price = articleVm.Price, Title = articleVm.Title};
            _db.Articles.Add(article);
            await _db.SaveChangesAsync();
            ArticleDescription description = new ArticleDescription()
            {
                ArticleId = article.Id,
                Color = articleVm.Color,
                Other = articleVm.Other,
                Size = articleVm.Size,
            };
            foreach (var image in articleVm.Images)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)image.Length);
                }
                // установка массива байтов
                ArticleImage img = new ArticleImage(){Image = imageData, ArticleId = article.Id};
                _db.ArticleImages.Add(img);
            }
            _db.ArticleDescriptions.Add(description);
            await _db.SaveChangesAsync();



           return RedirectToAction("Index", article.Id);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}