using OnlineShop.DTOs;

namespace OnlineShop.Services
{
    public interface IInventoryService
    {
        #region Properties
        public Task<bool> AddToInventory(InventoryDTO inventoryDTO, int userId);

        public Task<List<InventoryDTO>> GetAll(int userId);

        public Task<bool> Remove(InventoryDTO inventoryDTO, int userId);

        public Task<bool> EditQuantity(InventoryDTO inventoryDTO, int userId);
        #endregion
    }
}
