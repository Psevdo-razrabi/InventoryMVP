using System;
using UI;
using UnityEngine;

namespace Test
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;

        private void Update()
        {
            _inventory.InventoryStorage.InventoryModel.DataView.Coins.Value += 1;
        }
    }
}