using OnlineShop.DTOs;

namespace OnlineShop.Services
{
    public interface IOrderService
    {
        #region Public Methods
        public Task<bool> AddOrder(AddOrderDTO orderDTO, int userId);

        public Task<List<OrderDTO>> GetAll();

        public Task<OrderDTO?> GetById(int id);

        public Task<List<OrderDTO?>> GetByUserId(int userId);
        #endregion
    }
}
