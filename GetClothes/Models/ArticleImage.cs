namespace GetClothes.Models
{
    public class ArticleImage
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public byte[] Image { get; set; }
    }
}