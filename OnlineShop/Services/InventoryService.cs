using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Mappings;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class InventoryService : BaseService, IInventoryService
    {
        #region Constructors    
        public InventoryService(UnitOfWork unitOfWork, IAuthorizationService authService) : base(unitOfWork, authService) {  }

        #endregion

        #region Public Methods
        public async Task<bool> AddToInventory(InventoryDTO inventoryDTO, int userId)
        {
            Product? product = await _unitOfWork.Products.GetByName(inventoryDTO.ProductName);
            Shop? shop = await _unitOfWork.Shops.GetById(inventoryDTO.ShopId);

            if (product == null || shop == null)
            {
                return false;
            }

            if(product.IsDeleted == true)
            {
                return false;
            }

            Inventory? inventory = await _unitOfWork.Inventories.GetByShopIdAndProductId(shop.Id, product.Id);

            if (inventory == null)
            {
                Inventory newInventory = new()
                {
                    Product = product,
                    ProductId = product.Id,
                    Shop = shop,
                    ShopId = shop.Id,
                    Quantity = inventoryDTO.Quantity
                };

                await _unitOfWork.Inventories.Insert(newInventory);
            }
            else
            {
                inventory.Quantity = inventoryDTO.Quantity;
                _unitOfWork.Inventories.Update(inventory);
            }

            return await _unitOfWork.SaveChanges();
        }

        public async Task<bool> EditQuantity(InventoryDTO inventoryDTO, int userId)
        {
            var result = await _unitOfWork.Products.GetByName(inventoryDTO.ProductName);

            if (result == null)
            {
                return false;
            }

            Inventory? inventory = await _unitOfWork.Inventories.GetByShopIdAndProductId(inventoryDTO.ShopId, result.Id);

            if (inventory == null)
            {
                return false;
            }

            inventory.Quantity = inventoryDTO.Quantity;

            _unitOfWork.Inventories.Update(inventory);
            return await _unitOfWork.SaveChanges();
        }

        public async Task<Dictionary<string, List<InventoryDTO>>> GetAll(int userId)
        {
            return await _unitOfWork.Inventories.GetAll(userId);
        }

        public async Task<bool> Remove(InventoryDTO inventoryDTO, int userId)
        {
            var result = await _unitOfWork.Products.GetByName(inventoryDTO.ProductName);

            if(result == null)
            {
                return false;
            }

            Inventory? inventory = await _unitOfWork.Inventories.GetByShopIdAndProductId(inventoryDTO.ShopId, result.Id);

            if(inventory == null)
            {
                return false;
            }

            _unitOfWork.Inventories.Remove(inventory);
            return await _unitOfWork.SaveChanges();
        }
        #endregion
    }
}
