using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Card> _cardRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CardsInOrder> _cardsInOrderRepository;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Card> cardRepository, IRepository<Order> orderRepository, IRepository<CardsInOrder> cardsInOrderRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _cardRepository = cardRepository;
            _orderRepository = orderRepository;
            _cardsInOrderRepository = cardsInOrderRepository;
        }

        public ShoppingCart AddCardToShoppingCart(string userId, AddToCartDTO model)
        {

            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var selectedCard = _cardRepository.Get(model.SelectedCardId);

                if (selectedCard != null && userCart != null)
                {
                    userCart?.CardInShoppingCarts?.Add(new CardsInShoppingCart
                    {
                        Card = selectedCard,
                        CardId = selectedCard.Id,
                        ShoppingCart = userCart,
                        ShoppingCartId = userCart.Id,
                        Quantity = model.Quantity
                    });

                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);


                var product_to_delete = loggedInUser?.UserCart?.CardInShoppingCarts.First(z => z.CardId == Id);

                loggedInUser?.UserCart?.CardInShoppingCarts?.Remove(product_to_delete);

                _shoppingCartRepository.Update(loggedInUser.UserCart);

                return true;

            }

            return false;
        }

        public AddToCartDTO getProductInfo(Guid Id)
        {
            var selectedProduct = _cardRepository.Get(Id);
            if (selectedProduct != null)
            {
                var model = new AddToCartDTO
                {
                    SelectedCardName = selectedProduct.ExpansionId.ToString(),
                    SelectedCardId = selectedProduct.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var allProducts = loggedInUser?.UserCart?.CardInShoppingCarts?.ToList();

                var totalPrice = 0.0;

                foreach (var item in allProducts)
                {
                    totalPrice += Double.Round((item.Quantity * item.Card.Price), 2);
                }

                var model = new ShoppingCartDTO
                {
                    AllCards = allProducts,
                    TotalPrice = totalPrice
                };

                return model;

            }

            return new ShoppingCartDTO
            {
                AllCards = new List<CardsInShoppingCart>(),
                TotalPrice = 0.0
            };
        }


        public bool orderCardss(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(order);

                List<CardsInOrder> productInOrder = new List<CardsInOrder>();

                var lista = userShoppingCart.CardInShoppingCarts.Select(
                    x => new CardsInOrder
                    {
                        Id = Guid.NewGuid(),
                        CardId = x.Card.Id,
                        OrderedCard = x.Card,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();

                productInOrder.AddRange(lista);

                foreach (var product in productInOrder)
                {
                    _cardsInOrderRepository.Insert(product);
                }

                loggedInUser.UserCart.CardInShoppingCarts.Clear();
                _userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }
    }
}
