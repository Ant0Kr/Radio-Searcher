namespace RadioSearcher.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string ProductLink { get; set; }
        public string Cost { get; set; }
        public string IsAvailable { get; set; }
    }
}