using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class CardService : ICardService
    {
        private readonly IRepository<Card> _cardRepository;
        private readonly IUserRepository _userRepository;

        public CardService(IRepository<Card> cardRepository, IUserRepository userRepository)
        {
            _cardRepository = cardRepository;
            _userRepository = userRepository;
        }

        public Card CreateNewCard(string userId, Card Card)
        {
            var createdBy = _userRepository.Get(userId);
            Card.CreatedBy = createdBy;
            return _cardRepository.Insert(Card);

        }

        public Card DeleteCard(Guid id)
        {
            var card_to_delete = this.GetCardById(id);
            return _cardRepository.Delete(card_to_delete);
        }

        public Card GetCardById(Guid? id)
        {
            return _cardRepository.Get(id);
        }

        public List<Card> GetCards()
        {
            return _cardRepository.GetAll().ToList();
        }

        public Card UpdateCard(Card Card)
        {
            return _cardRepository.Update(Card);
        }
    }
}
