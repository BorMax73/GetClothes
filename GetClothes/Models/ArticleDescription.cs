namespace GetClothes.Models
{
    public class ArticleDescription
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Size { get; set; }

        public string Color { get; set; }

        public string Other { get; set; }
    }
}