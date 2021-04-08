using UnityEngine;
using UnityEngine.UI;


namespace BeastHunter
{
    public class HubMapUIEquipmentSlotBehaviour : HubMapUISlotBehaviour
    {
        [SerializeField] Image _slotImage;

        private Sprite _slotItemSprite;

        public override void FillSlotInfo(int slotIndex, bool isDragAndDropEnabled)
        {
            base.FillSlotInfo(slotIndex, isDragAndDropEnabled);
            _slotImage.enabled = true;
            _slotImage.sprite = _slotItemSprite;
        }

        public override void FillSlot(Sprite sprite)
        {
            base.FillSlot(sprite);

            if (sprite != null)
            {
                _slotImage.enabled = false;
            }
            else
            {
                _slotImage.enabled = true;
            }
        }
    }
}
