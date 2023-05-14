using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Mappings;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class OrderService : BaseService, IOrderService
    {
        #region Constructors
        public OrderService(UnitOfWork unitOfWork, IAuthorizationService authService)
            : base(unitOfWork, authService) { }
        #endregion

        #region Private Methods
        private async Task<double> AddProductToOrder(OrderedProductDTO orderedProductDTO, int orderID)
        {
            Product? product = await _unitOfWork.Products.GetByName(orderedProductDTO.ProductName);
            Shop? shop = await _unitOfWork.Shops.GetByName(orderedProductDTO.ShopName);

            if (product == null || shop == null || orderedProductDTO.Quantity == 0)
            {
                return 0;
            }

            if (!await _unitOfWork.Inventories.IsQuantityInInventory(shop.Id, product.Id, orderedProductDTO.Quantity))
            {
                return 0;
            }

            Inventory? inventory = await _unitOfWork.Inventories.GetByShopIdAndProductId(shop.Id, product.Id);

            if (inventory == null)
            {
                return 0;
            }

            inventory.Quantity -= orderedProductDTO.Quantity;
            _unitOfWork.Inventories.Update(inventory);

            OrderedProduct orderedProduct = new()
            {
                ProductId = product.Id,
                ShopId = shop.Id,
                Quantity = orderedProductDTO.Quantity,
                OrderId = orderID
            };

            await _unitOfWork.OrderedProducts.Insert(orderedProduct);
            await _unitOfWork.SaveChanges();
            return orderedProduct.Quantity * product.Price;
        }
        #endregion

        #region Public Methods
        public async Task<bool> AddOrder(AddOrderDTO orderDTO, int userId)
        {
            if (orderDTO == null)
            {
                return false;
            }

            Order order = new()
            {
                DateCreated = DateTime.Now,
                UserId = userId
            };

            order = await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.SaveChanges();

            foreach (OrderedProductDTO orderProduct in orderDTO.OrderedProducts)
            {
                order.TotalAmount += await AddProductToOrder(orderProduct, order.Id);
            }

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            List<Order> orders = await _unitOfWork.Orders.GetAll();
            List<OrderDTO> ordersDTO = new();

            foreach (Order order in orders)
            {
                OrderDTO orderDTO = new()
                {
                    TotalAmount = order.TotalAmount,
                    DateCreated = order.DateCreated,
                    OrderedProducts = new List<OrderedProductDTO>()
                };

                foreach (OrderedProduct orderedProduct in order.OrderedProducts)
                {
                    Product? product = await _unitOfWork.Products.GetById(orderedProduct.ProductId);
                    Shop? shop = await _unitOfWork.Shops.GetById((int)orderedProduct.ShopId!);

                    OrderedProductDTO orderedProductDTO = new()
                    {
                        ProductName = product == null ? "" : product.Name,
                        ShopName = shop == null ? "" : shop.Name,
                        Quantity = orderedProduct.Quantity
                    };

                    orderDTO.OrderedProducts.Add(orderedProductDTO);
                }

                ordersDTO.Add(orderDTO);
            }
            return ordersDTO;
        }

        public async Task<OrderDTO?> GetById(int id)
        {
            Order? order = await _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return null;
            }

            OrderDTO orderDTO = new()
            {
                TotalAmount = order.TotalAmount,
                DateCreated = order.DateCreated,
                OrderedProducts = new List<OrderedProductDTO>()
            };

            foreach (OrderedProduct orderedProduct in order.OrderedProducts)
            {
                Product? product = await _unitOfWork.Products.GetById(orderedProduct.ProductId);
                Shop? shop = await _unitOfWork.Shops.GetById((int)orderedProduct.ShopId!);

                OrderedProductDTO orderedProductDTO = new()
                {
                    ProductName = product == null ? "" : product.Name,
                    ShopName = shop == null ? "" : shop.Name,
                    Quantity = orderedProduct.Quantity
                };

                orderDTO.OrderedProducts.Add(orderedProductDTO);
            }
            return orderDTO;
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
