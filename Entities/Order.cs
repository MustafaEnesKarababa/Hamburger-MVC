namespace IdentitySonProje.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int AppUserId { get; set; }
        public double Price { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public AppUser AppUser { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
