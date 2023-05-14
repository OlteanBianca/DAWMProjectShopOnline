using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Mappings
{
    public static class InventoryDTOMapping
    {
        #region Entity To DTO

        public static List<InventoryDTO> ToInventoryDTOs(this List<Inventory> inventories)
        {
            inventories ??= new();

            return inventories.Select(e => e.ToInventoryDTO()).ToList();
        }

        public static InventoryDTO ToInventoryDTO(this Inventory inventory)
        {
            if (inventory == null) return new InventoryDTO();

            InventoryDTO inventoryDTO = new()
            {
                ShopName = inventory.Shop.Name,
                ProductName = inventory.Product.Name,
                Quantity = inventory.Quantity
            };

            return inventoryDTO;
        }
        #endregion
    }
}
