using IdentitySonProje.Enums;
using System.Reflection.PortableExecutable;

namespace IdentitySonProje.Entities
{
    public class OrderProduct
    {
        public int OrderProductId { get; set; }
        public double Price { get; set; }
        public Size Size { get; set; } = Size.Medium;
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
        public ICollection<Extra>? Extras { get; set; }
    }
}
