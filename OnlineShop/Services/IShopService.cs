using OnlineShop.DTOs;

namespace OnlineShop.Services
{
    public interface IShopService
    {
        #region Public Methods
        public Task<bool> AddShop(ShopDTO shopDTO, int userId);

        public Task<List<ShopDTO?>> GetAllShops();

        public Task<ShopDTO?> GetShopById(int id);

        public Task<List<ShopDTO?>> GetShopsByUserId(int id);

        public Task<bool> CheckIfShopNameExists(string name);
        #endregion
    }
}
