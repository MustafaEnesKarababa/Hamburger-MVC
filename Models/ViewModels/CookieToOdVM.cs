using IdentitySonProje.Entities;
using IdentitySonProje.Enums;

namespace IdentitySonProje.Models.ViewModels
{
    public class CookieToOdVM
    {
        public string cookieID { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public List<Extra> Extras { get; set; }
        public Size Size { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
