using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Mappings
{
    public static class ShopDTOMapping
    {
        #region DTO to Entity
        public static Shop? ToShop(this ShopDTO shopDTO)
        {
            if (shopDTO == null) return null;

            Shop shop = new()
            {
                Name = shopDTO.Name,
                Address = shopDTO.Address,
            };

            return shop;
        }
        #endregion

        #region Entity To DTO
        public static ShopDTO? ToShopDTO(this Shop shop)
        {
            if (shop == null) return null;

            ShopDTO shopDTO = new()
            {
                Name = shop.Name,
                Address = shop.Address,
            };

            return shopDTO;
        }

        public static List<ShopDTO?> ToShopDTOs(this List<Shop> shops)
        {
            shops ??= new List<Shop>();

            return shops.Select(shop => shop.ToShopDTO()).ToList();
        }
        #endregion
    }
}
