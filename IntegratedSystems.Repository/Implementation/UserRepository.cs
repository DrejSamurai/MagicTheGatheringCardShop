using IntegratedSystems.Domain.Identity_Models;
using IntegratedSystems.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<IntegratedSystemsUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<IntegratedSystemsUser>();
        }
        public IEnumerable<IntegratedSystemsUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IntegratedSystemsUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.CardInShoppingCarts)
                .Include("UserCart.CardInShoppingCarts.Card")
                .First(s => s.Id == strGuid);
        }
        public void Insert(IntegratedSystemsUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(IntegratedSystemsUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(IntegratedSystemsUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
