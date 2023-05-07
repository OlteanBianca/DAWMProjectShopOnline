using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(UnitOfWork unitOfWork, IAuthorizationService authService) : base(unitOfWork, authService) { }

        public Task<bool> AddOrder(List<AddOrderDTO> orderDTO)
        {
            //foreach(AddOrderDTO order in orderDTO)
            //{
            //    int shopId = _unitOfWork.Sho
            //}
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByID(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
