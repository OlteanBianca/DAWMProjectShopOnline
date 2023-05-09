using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public interface IOrderService
    {
        #region Properties
        public Task<bool> AddOrder(AddOrderDTO orderDTO, int userId);

        public Task<List<OrderDTO>> GetAll();

        public Task<OrderDTO?> GetById(int id);

        public Task<List<OrderDTO?>> GetByUserId(int userId);
        #endregion
    }
}
