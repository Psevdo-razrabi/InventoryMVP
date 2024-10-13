using System;
using CustomObserverable;
using R3;
using UI.Custom;
using UI.M;
using UI.V;

namespace UI.P
{
    public class InventoryPresenter : IDisposable
    {
        private readonly InventoryView _inventoryView;
        private readonly InventoryModel _inventoryModel;
        private readonly int _capacity;
        private CompositeDisposable _compositeDisposable = new();

        public InventoryPresenter(InventoryView inventoryView, InventoryModel inventoryModel, int capacity)
        {
            Preconditions.CheckNotNull(inventoryView, "View Is Null");
            Preconditions.CheckNotNull(inventoryModel, "Model Is Null");
            
            _inventoryView = inventoryView;
            _inventoryModel = inventoryModel;
            _capacity = capacity;
        }

        public void Initialize()
        {
            //действия с VIEW
            _inventoryModel.OnModelChange.Subscribe(_ => HandleModelChange()).AddTo(_compositeDisposable);
            _inventoryModel.DataView.Coins.Subscribe(_ =>
                _inventoryView.SetCapacity($"Money: {_inventoryModel.DataView.Coins}")).AddTo(_compositeDisposable);
        }

        private void HandleModelChange()
        {
            for (int i = 0; i < _capacity; i++)
            {
                var item = _inventoryModel.Get(i);
                if (item == null || item.Id.Equals(GuidItem.IsEmpty()))
                {
                    //view код
                }
                else
                {
                    //view код
                }
            }
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}