using IdentitySonProje.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Eventing.Reader;

namespace IdentitySonProje.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public SelectList Categories { get; set; }
        public IFormFile productImage { get; set; }
    }
}
