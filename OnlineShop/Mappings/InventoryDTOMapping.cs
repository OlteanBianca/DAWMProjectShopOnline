using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Mappings
{
    public static class InventoryDTOMapping
    {
        #region Entity To DTO

        public static List<InventoryDTO> ToInventoryDTOs(this List<Inventory> inventory)
        {
            var results = inventory.Select(e => e.ToInventoryDTO()).ToList();

            if (results.Any())
            {
                return results;
            }
            return new List<InventoryDTO>();
        }

        public static InventoryDTO ToInventoryDTO(this Inventory inventory)
        {
            if (inventory == null) return new InventoryDTO();

            InventoryDTO inventoryDTO = new()
            {
                ShopId = inventory.ShopId,
                ProductName = inventory.Product.Name,
                Quantity = inventory.Quantity
            };

            return inventoryDTO;
        }
        #endregion
    }
}
