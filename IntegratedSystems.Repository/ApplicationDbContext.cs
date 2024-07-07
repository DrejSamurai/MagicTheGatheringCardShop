using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.Identity_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntegratedSystems.Repository
{
    public class ApplicationDbContext : IdentityDbContext<IntegratedSystemsUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards{ get; set; }
        public virtual DbSet<Expansion> Expansions{ get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<CardsInShoppingCart> CardsInShoppingCarts { get; set; }
        public virtual DbSet<CardsInOrder> CardsInOrders { get; set; }

    }
}
