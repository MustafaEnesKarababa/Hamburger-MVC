using IdentitySonProje.Data;
using IdentitySonProje.Entities;
using IdentitySonProje.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Collections;
using IdentitySonProje.Enums;
using IdentitySonProje.Migrations;
using Humanizer;
using NuGet.Protocol;
using Microsoft.Extensions.Hosting;

namespace IdentitySonProje.Controllers
{
    [AllowAnonymous]
    public class HamburgerController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public HamburgerController(ApplicationDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {

            return View(dbContext.Products.Include(c => c.Category).ToList());
        }

        public IActionResult AddCart(int productId)
        {
            OrderDetailVM orderDetailVM = new();
            orderDetailVM.Product = dbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
            orderDetailVM.Extras = new SelectList(dbContext.Extras, "ExtraId", "Name");

            return View(orderDetailVM);
        }

        [HttpPost]
        public IActionResult AddCart(OrderDetailVM orderDetailVM, string[] selectedExtras, string inlineRadioOptions)
        {
            string extras = string.Empty;

            double extraCost = 0;
            double cost = 0;

            for (int i = 0; i < selectedExtras.Length; i++)
            {
                if (i == selectedExtras.Length - 1)
                {
                    extras += selectedExtras[i];
                }
                else
                {
                    extras += selectedExtras[i] + "+";
                }
                extraCost += dbContext.Extras.Find(int.Parse(selectedExtras[i])).Price;
            }

            if (inlineRadioOptions != null)
            {
                if (inlineRadioOptions == "1")
                {
                    orderDetailVM.OrderProduct.Size = Enums.Size.Small;
                    cost = orderDetailVM.Product.Price * 0.75;
                }
                if (inlineRadioOptions == "2")
                {
                    orderDetailVM.OrderProduct.Size = Enums.Size.Medium;
                    cost = orderDetailVM.Product.Price;
                }
                if (inlineRadioOptions == "3")
                {
                    orderDetailVM.OrderProduct.Size = Enums.Size.Large;
                    cost = orderDetailVM.Product.Price * 1.25;
                }
            }

            Guid guid = Guid.NewGuid();
            string value = "productId" + "=" + orderDetailVM.Product.ProductId + "," + " " + "price" + "=" + (cost + extraCost) * orderDetailVM.OrderProduct.Quantity + "," + " " + "extras" + "=" + extras + "," + " " + "size" + "=" + orderDetailVM.OrderProduct.Size.ToString() + "," + " " + "quantity" + "=" + orderDetailVM.OrderProduct.Quantity + "," + " " + "cookieId" + "=" + guid.ToString();

            string cookieValue = Request.Cookies["ShoppingCart"];

            if (!string.IsNullOrEmpty(cookieValue))
            {
                cookieValue += "&" + value;
                Response.Cookies.Delete("ShoppingCart");
            }
            else
            {
                cookieValue = value;
            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.HttpOnly = true;
            options.Secure = true;
            Response.Cookies.Append("ShoppingCart", cookieValue, options);

            return RedirectToAction("Index");
        }

        public IActionResult ShoppingCart()
        {
            string cookieValue = Request.Cookies["ShoppingCart"];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                var list = MakeItBecomeShoppingCart(cookieValue);
                double price = 0;
                foreach (var item in list)
                {
                    price += item.Price;
                }
                ViewBag.price = price;
                return View(list);
            }
            return View("EmptyShoppingCart");
        }

        [HttpPost]
        [Authorize]
        public IActionResult ShoppingCart(List<CookieToOdVM> cookieToOdVMs)
        {
            var user = userManager.GetUserAsync(User).Result;

            if(signInManager.IsSignedIn(User))
            {
                Response.Cookies.Delete("ShoppingCart");

                List<OrderProduct> OrderDetails = new List<OrderProduct>();
                double totalCost = 0;
                foreach (var item in cookieToOdVMs)
                {
                    totalCost += item.Price;
                }

                Order order = new Order();
                order.AppUserId = user.Id;
                order.Price = totalCost;

                if(dbContext.SaveChanges() > 0)
                {
                    foreach (var item in cookieToOdVMs)
                    {
                        OrderProduct od = new OrderProduct();
                        foreach (OrderProduct orderDetail in OrderDetails)
                        {
                            od.Price = item.Price;
                            od.Size = item.Size;
                            od.Quantity = item.Quantity;
                            od.OrderId = order.OrderId;
                            od.ProductId = item.ProductId;
                            od.Extras = item.Extras;
                        }
                    }
                    if(dbContext.SaveChanges() > 0)
                    {
                        return View("OrderConfirmed");
                    }
                }
                return View("ErrorPage");
            }

            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        public IActionResult RemoveFromCart(string cookieId)
        {
            string cookieValue = Request.Cookies["ShoppingCart"];
            var list = MakeItBecomeShoppingCart(cookieValue);
            var orderToRemove = list.FirstOrDefault(a => a.cookieID == cookieId);
            if (orderToRemove is not null)
            {
                bool isSuccees = list.Remove(list.FirstOrDefault(a => a.cookieID == cookieId));
                if (isSuccees)
                {
                    ToCookie(list, "ShoppingCart");
                    return RedirectToAction("ShoppingCart");
                }
                return View("ErrorPage");
            }
            else
            {
                return View("ErrorPage");
            }
        }

        public IActionResult OrderConfirmed()
        {
            return View();
        }

        public IActionResult ErrorPage()
        {
            return View();
        }

        private List<CookieToOdVM> MakeItBecomeShoppingCart(string cookieValue)
        {
            List<CookieToOdVM> orderDetailsList = new List<CookieToOdVM>();
            List<Extra> extrasOfVm = new List<Extra>();

            var firstList = cookieValue.Split('&');
            foreach (var item in firstList)
            {
                extrasOfVm.Clear();
                string newItem = item.Replace(" ", string.Empty).Replace(",", "=");

                var secondList = newItem.Split('=');

                CookieToOdVM vm = new CookieToOdVM();
                vm.ProductName = dbContext.Products.Find(int.Parse(secondList[1])).Name;
                vm.ProductId = int.Parse(secondList[1]);
                vm.Price = double.Parse(secondList[3]);
                vm.Quantity = int.Parse(secondList[9]);
                vm.cookieID = secondList[11];

                var extraList = secondList[5].Split('+');

                foreach (string extraIdString in extraList)
                {
                    int extraId = int.Parse(extraIdString);
                    extrasOfVm.Add(dbContext.Extras.Find(extraId));
                }

                vm.Extras = extrasOfVm;

                if (secondList[7] == "Small")
                {
                    vm.Size = Size.Small;
                    vm.ProductPrice = dbContext.Products.Find(int.Parse(secondList[1])).Price * 0.75;
                }
                else if (secondList[7] == "Medium")
                {
                    vm.Size = Size.Medium;
                    vm.ProductPrice = dbContext.Products.Find(int.Parse(secondList[1])).Price;
                }
                else
                {
                    vm.Size = Size.Large;
                    vm.ProductPrice = dbContext.Products.Find(int.Parse(secondList[1])).Price * 1.25;
                }

                orderDetailsList.Add(vm);
            }

            return orderDetailsList;
        }

        private void ToCookie(List<CookieToOdVM> list, string cookieName)
        {
            //string cookieValue = Request.Cookies[cookieName];
            string cookieValue = string.Empty;
            Response.Cookies.Delete(cookieName);

            foreach (var item in list)
            {
                string extras = string.Empty;
                for (int i = 0; i < item.Extras.Count; i++)
                {
                    if (i == item.Extras.Count - 1)
                    {
                        extras += item.Extras[i].ExtraId;
                    }
                    else
                    {
                        extras += item.Extras[i].ExtraId + "+";
                    }
                }
                string value = "productId" + "=" + item.ProductId + "," + " " + "price" + "=" + item.Price + "," + " " + "extras" + "=" + extras + "," + " " + "size" + "=" + item.Size.ToString() + "," + " " + "quantity" + "=" + item.Quantity + "," + " " + "cookieId" + "=" + item.cookieID;

                if(string.IsNullOrEmpty(cookieValue))
                {
                    cookieValue = value;
                }
                else
                {
                    cookieValue += "&" + value;
                }
            }
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.HttpOnly = true;
            options.Secure = true;
            Response.Cookies.Append(cookieName, cookieValue, options);
        }
    }
}
