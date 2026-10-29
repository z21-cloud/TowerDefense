using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.Core
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetImage(Image image)
        {
            _image = image;
        }
    }
}
