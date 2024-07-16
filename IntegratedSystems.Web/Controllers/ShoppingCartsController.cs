using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using System.Security.Claims;
using ClosedXML.Excel;
using IntegratedSystems.Service.Implementation;
using Microsoft.AspNetCore.Authorization;

namespace IntegratedSystems.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            return View(_shoppingCartService.getShoppingCartDetails(userId ?? ""));
        }

        // GET: ShoppingCarts/Details/5

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> DeleteProductFromShoppingCart(Guid? productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.deleteFromShoppingCart(userId, productId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.orderCardss(userId ?? "");

            return RedirectToAction("Index", "ShoppingCarts");
        }

        [HttpGet]
        [Authorize]
        public FileContentResult ExportAllUserCardOrders()
        {
            string fileName = "CardOrders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("CardOrders");
                worksheet.Cell(1, 1).Value = "Order Id";
                worksheet.Cell(1, 2).Value = "Owner Username";
                worksheet.Cell(1, 3).Value = "Total Cards";
                worksheet.Cell(1, 4).Value = "Cards";

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var data = _orderService.GetAllUserOrders(userId);

                for (int i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    worksheet.Cell(i + 2, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = item.Owner.UserName;
                    worksheet.Cell(i + 2, 3).Value = item.CardsInOrders.Count.ToString();
                    for (int j = 0; j < item.CardsInOrders.Count(); j++)
                    {
                        worksheet.Cell(i+2, 4).Value += "Card - " + (j + 1) + "\n" + "Card Name: "
                            + item.CardsInOrders.ElementAt(j).OrderedCard.CardName + "\n"
                            + "Card Description:" + item.CardsInOrders.ElementAt(j).OrderedCard.CardDescription + "\n"
                            + "Expansion Name:" + item.CardsInOrders.ElementAt(j).OrderedCard.Expansion.ExpansionName + "\n";
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }

    }
}
