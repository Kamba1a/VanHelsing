using UnityEngine;
using UnityEngine.UI;


namespace BeastHunter
{
    public class HubMapUIEquipmentSlotBehaviour : HubMapUISlotBehaviour
    {
        [SerializeField] Image _slotImage;

        public void FillSlotInfo(int slotIndex, bool isDragAndDropEnabled, Sprite slotSprite)
        {
            base.FillSlotInfo(slotIndex, isDragAndDropEnabled);
            _slotImage.sprite = slotSprite;
            _slotImage.enabled = true;
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
