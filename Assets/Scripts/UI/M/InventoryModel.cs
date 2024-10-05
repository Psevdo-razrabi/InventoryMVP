using System;
using System.Collections.Generic;
using CustomObserverable;
using UI.Custom;
using UniRx;

namespace UI.M
{
    public class InventoryModel
    {
        public readonly ObservableArray<Item> Items;
        private int _capasity;

        private readonly Subject<Item[]> _onModelChange = new ();
        
        public InventoryModel(IEnumerable<ItemDescription> items, int capacity)
        {
            Preconditions.CheckValidateData(capacity);
            Items = new ObservableArray<Item>(capacity);

            foreach (var item in items)
            {
                Items.TryAdd(Factory.CreateItem(item, 1));
            }

            Subscribe();
        }

        private void Subscribe()
        {
            Items.ValueChangeInArray.Subscribe(items => _onModelChange.OnNext(items));
        }

        public Item Get(int index) => Items[index];
        public bool Add(Item item) => Items.TryAdd(item);
        public bool Remove(Item item) => Items.TryRemove(item);
        public void Clear() => Items.Clear();
        public void Swap(int indexOne, int indexTwo) => Items.Swap(indexOne, indexTwo);

        public int CombineItem(int indexOne, int indexTwo)
        {
            var totalQuantity = Items[indexOne].Quantity + Items[indexTwo].Quantity;
            Items[indexTwo].SetQuantity(totalQuantity);
            Remove(Items[indexOne]);
            return totalQuantity;
        }
    }
}