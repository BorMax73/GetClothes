using GetClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GetClothes.Data;
using GetClothes.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace GetClothes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;
        public HomeController(ApplicationContext db,  ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var articles = await _db.Articles.ToListAsync();
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

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Subscriber(NewsletterSubscriber subscriber)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _db.Subscribers.AddAsync(subscriber);
           await _db.SaveChangesAsync();
           return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Question()
        {
            return View("support");
        }
        [HttpPost]
        public async Task<IActionResult> Question(Question question)
        {
            if (!ModelState.IsValid)
                BadRequest(question);

            await _db.Questions.AddAsync(question);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
