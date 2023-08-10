using IdentitySonProje.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentitySonProje.Models.ViewModels
{
    public class OrderDetailVM
    {
        public Product Product { get; set; }
        public SelectList Extras { get; set; }
        public OrderProduct OrderProduct { get; set; }
    }
}
