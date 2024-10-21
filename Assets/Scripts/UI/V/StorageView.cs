using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory
{
    public abstract class StorageView : MonoBehaviour
    {
        [field: SerializeField] protected RectTransform listSlots;
        [field: SerializeField] protected ViewSlot prefabSlots;
        [field: SerializeField] protected GridLayoutGroup grid;
        [field: SerializeField] protected ViewSlot GhostIcon;
        protected Canvas canvas;
        public ViewSlot[] Slots { get; set; }
        public event Action<ViewSlot, ViewSlot> OnDrop;
        public abstract UniTask InitializeView(DataView dataView);

        private void Start()
        {
            canvas = GetComponent<Canvas>();
        }

        public void OnActiveGrid(bool isActive)
        {
            grid.enabled = isActive;
        }

        protected void InvokeDrop(ViewSlot originalSlot, ViewSlot closestSlot)
        {
            OnDrop?.Invoke(originalSlot, closestSlot);
        }
    }
}