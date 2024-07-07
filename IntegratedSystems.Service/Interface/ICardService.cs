using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface ICardService
    {
        public List<Card> GetCards();
        public Card GetCardById(Guid? id);
        public Card CreateNewCard(string userId, Card Card);
        public Card UpdateCard(Card Card);
        public Card DeleteCard(Guid id);
    }
}
