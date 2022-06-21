namespace GetClothes.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }

        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}