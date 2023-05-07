using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public interface IOrderService
    {
        public Task<bool> AddOrder(List<AddOrderDTO> orderDTO);
        public Task<IEnumerable<Order>> GetAllOrders();
        public Task<bool> Update(Order order);
        public Task<bool> Remove(Order order);
        public Task<Order> GetOrderByID(int? id);
    }
}
