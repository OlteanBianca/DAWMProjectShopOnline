using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Mappings;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class OrderService : BaseService, IOrderService
    {
        #region Constructors
        public OrderService(UnitOfWork unitOfWork, IAuthorizationService authService) : base(unitOfWork, authService) { }
        #endregion

        #region Public Methods
        public Task<bool> AddOrder(List<AddOrderDTO> orderDTO)
        {
            //foreach(AddOrderDTO order in orderDTO)
            //{
            //    int shopId = _unitOfWork.Sho
            //}
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _unitOfWork.Orders.GetAll();
        }

        public Task<bool> Update(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDTO?> GetById(int id)
        {
            Order? order = await _unitOfWork.Orders.GetById(id);

            return order?.ToOrderDTO();
        }

        public async Task<List<OrderDTO?>> GetByUserId(int userId)
        {
            List<Order> orders = await _unitOfWork.Orders.GetByUserId(userId);
            orders ??= new();

            return orders.ToOrderDTOs();
        }
        #endregion
    }
}
