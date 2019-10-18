using System.Linq;
using BigDeckPlays.DAL.Repositories;

namespace BigDeckPlays.ApplicationCore.Services
{
    public interface ICollectionService
    {
        void GetCards();
        void TransferCards();
    }
    
    public class CollectionService : ICollectionService
    {
        private IInventoryRepository _inventory;
        private IConsoleRepository _console;
        
        public CollectionService(IInventoryRepository inventory = null,
                                 IConsoleRepository console = null)
        {
            _inventory = inventory ?? new InventoryRepository();
            _console = console ?? new ConsoleRepository();
        }
        
        public void GetCards()
        {
            foreach(var card in _inventory.GetCards().OrderBy(c => c.Name))
            {
                _console.WriteLine($"{card.Quantity} | {card.Name}");
            }
        }

        public void TransferCards()
        {
            _inventory.AddCards(_inventory.GetCards().OrderBy(c => c.Name));
        }
    }
}
