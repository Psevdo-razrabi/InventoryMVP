using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.V
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coins;


        public void AddCapacity(string text)
        {
            
        }

        public void SetCapacity(string value)
        {
            coins.text = value;
        }
    }
}