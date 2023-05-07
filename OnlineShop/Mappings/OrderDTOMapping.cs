using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Mappings
{
    public static class OrderDTOMapping
    {
        #region DTO to Entity
        public static Order? ToOrder(this OrderDTO orderDTO)
        {
            if (orderDTO == null) return null;

            Order order = new()
            {
                DateCreated = orderDTO.DateCreated,
                TotalAmount = orderDTO.TotalAmount,
            };

            return order;
        }
        #endregion

        #region Entity To DTO
        public static OrderDTO? ToOrderDTO(this Order order)
        {
            if (order == null) return null;

            OrderDTO orderDTO = new()
            {
                DateCreated = order.DateCreated,
                TotalAmount = order.TotalAmount,
            };

            return orderDTO;
        }

        public static List<OrderDTO?> ToOrderDTOs(this List<Order> orders)
        {
            orders ??= new List<Order>();

            return orders.Select(order => order.ToOrderDTO()).ToList();
        }
        #endregion
    }
}
