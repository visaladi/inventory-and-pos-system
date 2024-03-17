//using IMS.CoreBusiness;
//using IMS.UseCases.PluginInterfaces;

//namespace IMS.Plugins.InMemory
//{
//    public class InventoryRepository : IInventoryRepository
//    {
//        private List<Inventory> _inventories;   // Variable from a List of Inventories

//        public InventoryRepository()
//        {
//            // New object from the List of Inventories is assigned to the Variable from List of Inventories 
//            _inventories = new List<Inventory>()
//            {
//                new Inventory{ InventoryID=1, InventoryName="Bike Seat",   Quantity=10, Price=2 },
//                new Inventory{ InventoryID=2, InventoryName="Bike Body",  Quantity=10, Price=15 },
//                new Inventory{ InventoryID=3, InventoryName="Bike Wheels", Quantity=20, Price=8 },
//                new Inventory{ InventoryID=4, InventoryName="Bike Pedels", Quantity=20, Price=1 }
//            };
//        }

//        //public async Task<bool> ExistsAsync(Inventory inventory)
//        //{
//        //    return await Task.FromResult(_inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)));
//        //}

//        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
//        {
//            //throw new NotImplementedException(); // Not needed for now
//            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);

//            return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));

//        }

//        public Task AddInventoryAsync(Inventory inventory)
//        {
//            if (_inventories.Any(x=>x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
//            {
//                return Task.CompletedTask;
//            }

//            var maxId = _inventories.Max(x => x.InventoryID);
//            inventory.InventoryID = maxId+1;
            
//            _inventories.Add(inventory);
//            return Task.CompletedTask;
            
//        }

//        public Task UpdateInventoryAsync(Inventory inventory)
//        {
//            if (_inventories.Any(x => x.InventoryID == inventory.InventoryID &&
//                                 x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
//            { return Task.CompletedTask; }

//            var inv = _inventories.FirstOrDefault(x=>x.InventoryID == inventory.InventoryID);
//            if(inv != null)
//            {
//                inv.InventoryName = inventory.InventoryName;
//                inv.Quantity = inventory.Quantity;
//                inv.Price = inventory.Price;
//            }

//            return Task.CompletedTask;
//        }

//        public async Task<Inventory> GetInventoriesByIdAsync(int inventoryId)
//        {
//            return await Task.FromResult(_inventories.First(x => x.InventoryID == inventoryId));
//        }

//        //Task IInventoryRepository.AddInventoryAsync(Inventory inventory)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //Task IInventoryRepository.UpdateInventoryAsync(Inventory inventory)
//        //{
//        //    throw new NotImplementedException();
//        //}
//    }
//}
