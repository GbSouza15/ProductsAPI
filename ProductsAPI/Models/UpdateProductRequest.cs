namespace ProductsAPI.Models
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Value { get; set; }
    }
}
