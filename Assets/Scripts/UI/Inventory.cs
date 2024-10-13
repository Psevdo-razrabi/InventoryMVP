using System.Collections.Generic;
using Builder;
using UI.V;
using UnityEngine;

namespace UI
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private int capacity;
        [SerializeField] private List<ItemDescription> startingItem;
        private BuildInventory _build;
        
        public InventoryStorage InventoryStorage { get; private set; }

        private void Awake()
        {
            _build = new BuildInventory(_inventoryView);
            InventoryStorage = _build
                .WithStartingItem(startingItem)
                .WithCapacity(capacity)
                .Build();
            
            InventoryStorage.InventoryPresenter.Initialize();
        }
    }
}