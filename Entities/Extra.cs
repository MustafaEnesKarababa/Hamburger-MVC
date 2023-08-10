namespace IdentitySonProje.Entities
{
    public class Extra
    {
        public int ExtraId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageName { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
