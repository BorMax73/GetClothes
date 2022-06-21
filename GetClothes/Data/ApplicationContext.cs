using GetClothes.Models;
using Microsoft.EntityFrameworkCore;

namespace GetClothes.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleDescription> ArticleDescriptions { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
        public DbSet<NewsletterSubscriber> Subscribers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

    }
}