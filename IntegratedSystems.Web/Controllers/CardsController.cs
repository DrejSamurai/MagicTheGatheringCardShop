using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Web.Controllers
{
    public class CardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICardService _cardService;

        public CardsController(ApplicationDbContext context, IShoppingCartService shoppingCartService, ICardService cardService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
            _cardService = cardService;
        }



        // GET: Cards
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cards.Include(c => c.Expansion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.Expansion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            ViewData["ExpansionId"] = new SelectList(_context.Set<Expansion>(), "Id", "ExpansionDescription");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpansionId,CardName,CardDescription,CardImage,Price,Rating,Rarity,Id")] Card card)
        {
            if (ModelState.IsValid)
            {
                card.Id = Guid.NewGuid();
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpansionId"] = new SelectList(_context.Set<Expansion>(), "Id", "ExpansionDescription", card.ExpansionId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["ExpansionId"] = new SelectList(_context.Set<Expansion>(), "Id", "ExpansionDescription", card.ExpansionId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExpansionId,CardName,CardDescription,Price,Rating,Rarity,Id")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpansionId"] = new SelectList(_context.Set<Expansion>(), "Id", "ExpansionDescription", card.ExpansionId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.Expansion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(Guid id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }

        [Authorize]
        public IActionResult AddProductToCart(Guid Id)
        {
            var result = _shoppingCartService.getProductInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddProductToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var result = _shoppingCartService.AddCardToShoppingCart(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }
        }


        [HttpPost("[action]")]
        public bool ImportAllCards(List<Card> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var card = new Card
                {
                    CardDescription = item.CardDescription,
                    CardName = item.CardName,
                    CardImage = item.CardImage
                };
                _cardService.CreateNewCard(null, card);
            }
            return status;
        }


        private bool ProductExists(Guid id)
        {
            return _cardService.GetCardById(id) != null;
        }
    }
}
