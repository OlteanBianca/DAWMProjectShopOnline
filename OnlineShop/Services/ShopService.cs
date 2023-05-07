using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Mappings;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class ShopService : BaseService, IShopService
    {
        #region Constructors
        public ShopService(UnitOfWork unitOfWork, IAuthorizationService authService) : base(unitOfWork, authService)
        {
        }
        #endregion

        #region Public Methods
        public async Task<List<ShopDTO?>> GetAllShops()
        {
            List<Shop> shops = await _unitOfWork.Shops.GetAll();
            shops ??= new();

            return shops.ToShopDTOs();
        }

        public async Task<bool> AddShop(ShopDTO shopDTO, int userId)
        {
            if (shopDTO == null || await CheckIfShopNameExists(shopDTO.Name))
            {
                return false;
            }

            Shop? shop = shopDTO.ToShop();
            if (shop == null)
            {
                return false;
            }

            shop.UserId = userId;
            await _unitOfWork.Shops.Insert(shop);
            return await _unitOfWork.SaveChanges();
        }

        public async Task<bool> CheckIfShopNameExists(string name)
        {
            return await _unitOfWork.Shops.CheckIfShopNameExists(name);
        }

        public async Task<ShopDTO?> GetShopById(int id)
        {
            Shop? shop = await _unitOfWork.Shops.GetById(id);
            return shop?.ToShopDTO();
        }

        public async Task<List<ShopDTO?>> GetShopsByUserId(int id)
        {
            List<Shop> shops = await _unitOfWork.Shops.GetShopsByUserId(id);
            shops ??= new();

            return shops.ToShopDTOs();
        }
        #endregion
    }
}
