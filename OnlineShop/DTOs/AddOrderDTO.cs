namespace OnlineShop.DTOs
{
    public class AddOrderDTO
    {

        public List<OrderedProductDTO> OrderedProducts { get; set; } = new();
    }
}
