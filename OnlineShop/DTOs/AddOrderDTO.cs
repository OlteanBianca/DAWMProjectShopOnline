namespace OnlineShop.DTOs
{
    public class AddOrderDTO
    {
        #region Properties
        public List<OrderedProductDTO> OrderedProducts { get; set; } = new();
        #endregion
    }
}
