using OnlineShop.DTOs;

namespace OnlineShop.Services
{
    public interface IInventoryService
    {
        #region Public Methods
        public Task<bool> AddToInventory(InventoryDTO inventoryDTO);

        public Task<Dictionary< string, List<InventoryDTO>>> GetAll(int userId);

        public Task<bool> Remove(InventoryDTO inventoryDTO);

        public Task<bool> EditQuantity(InventoryDTO inventoryDTO);
        #endregion
    }
}
