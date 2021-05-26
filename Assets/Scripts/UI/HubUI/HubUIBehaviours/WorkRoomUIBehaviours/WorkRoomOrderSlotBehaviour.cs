using System;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomOrderSlotBehaviour : BaseSlotBehaviour<OrderStorageType>
    {
        [SerializeField] private Button _removeOrderButton;


        public Action<int> OnClickRemoveOrderButtonHandler { get; set; }


        public override void Initialize(int slotIndex, OrderStorageType storageType, bool isDragAndDropOn)
        {
            base.Initialize(slotIndex, storageType, isDragAndDropOn);
            _removeOrderButton.onClick.AddListener(OnClickRemoveOrderButton);
            SetActiveRemoveOrderButton(false);
        }

        public override void FillSlot(Sprite sprite)
        {
            base.FillSlot(sprite);
            SetActiveRemoveOrderButton(sprite!=null);
        }

        private void SetActiveRemoveOrderButton(bool flag)
        {
            _removeOrderButton.gameObject.SetActive(flag);
        }

        private void OnClickRemoveOrderButton()
        {
            OnClickRemoveOrderButtonHandler?.Invoke(_slotIndex);
        }
    }
}
