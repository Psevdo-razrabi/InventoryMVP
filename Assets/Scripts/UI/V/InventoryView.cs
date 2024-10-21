using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryView : StorageView
    {
        private RectTransform _rectTransform;
        public override async UniTask InitializeView(DataView dataView)
        {
            InitializeSlots(dataView);
            await UniTask.Yield();
        }
        
        private void InitializeSlots(DataView dataView)
        {
            Slots = new ViewSlot[dataView.Capacity];
            
            ClearSlots();
            for (int i = 0; i < Slots.Length; i++)
            {
                var slot = Instantiate(prefabSlots, listSlots);
                Slots[i] = slot;    
                Slots[i].SetIndex(i);
                var eventTrigger = Slots[i].AddComponent<EventTrigger>();
                AddEventTrigger(EventTriggerType.BeginDrag, (eventData) => OnBeginDrag((PointerEventData)eventData), eventTrigger);
                AddEventTrigger(EventTriggerType.Drag, (eventData) => OnDrag((PointerEventData)eventData), eventTrigger);
                AddEventTrigger(EventTriggerType.EndDrag, (eventData) => OnEndDrag((PointerEventData)eventData), eventTrigger);
            }

            var initGhostIcon = Instantiate(GhostIcon, listSlots);
            _rectTransform = initGhostIcon.GetComponent<RectTransform>();
            GhostIcon = initGhostIcon;
            GhostIcon.gameObject.SetActive(false);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(listSlots);
        }

        private void ClearSlots()
        {
            var slots = listSlots.GetComponentsInChildren<ViewSlot>();

            foreach (var slot in slots)
            {
                Destroy(slot);
            }
        }
        
        private void OnBeginDrag(PointerEventData handler)
        {
            if (handler.pointerClick.TryGetComponent(out ViewSlot slot))
            {
                GhostIcon.SetImage(slot.Sprite);
                GhostIcon.SetGuid(slot.GuidItem);
                GhostIcon.SetIndex(slot.Index);
                GhostIcon.SetStackLabel(slot._stackLabel.text);
                slot.Clear();
                GhostIcon.gameObject.SetActive(true);
                _rectTransform.anchoredPosition = slot.GetComponent<RectTransform>().anchoredPosition;
            }
        }

        private void OnDrag(PointerEventData handler)
        {
            _rectTransform.anchoredPosition += handler.delta / canvas.scaleFactor;
        }

        private void OnEndDrag(PointerEventData handler)
        {
            GhostIcon.gameObject.SetActive(false);
            
            ViewSlot closestSlot = FindClosestSlot(handler.position);
            
            if (closestSlot != null)
                InvokeDrop(GhostIcon, closestSlot);
            else
            {
                Slots[GhostIcon.Index].SetGuid(GhostIcon.GuidItem);
                Slots[GhostIcon.Index].SetImage(GhostIcon.Image.sprite);
                Slots[GhostIcon.Index].SetStackLabel(GhostIcon._stackLabel.text);
            }
        }

        private ViewSlot FindClosestSlot(Vector2 position)
        {
            foreach (var slot in Slots)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(slot.GetComponent<RectTransform>(), position, canvas.worldCamera))
                {
                    return slot;
                }
            }

            return null;
        }

        private void AddEventTrigger(EventTriggerType eventTriggerType, Action<BaseEventData> callback, EventTrigger eventTrigger)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = eventTriggerType
            };
            entry.callback.AddListener(callback.Invoke);
            eventTrigger.triggers.Add(entry);
        }
    }
}