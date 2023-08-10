namespace IdentitySonProje.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageName { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
