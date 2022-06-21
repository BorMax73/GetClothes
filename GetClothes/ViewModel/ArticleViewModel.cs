using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace GetClothes.ViewModel
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double Price { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string Other { get; set; }
        public List<IFormFile> Images { get; set; }
        public byte[] Image { get; set; }
    }
}