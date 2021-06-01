using System;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomOrderSlotBehaviour : BaseSlotBehaviour<OrderStorageType>
    {
        [SerializeField] private Button _openRecipeBookButton;
        [SerializeField] private Button _removeOrderButton;
        [SerializeField] private GameObject _processImage;
        [SerializeField] private Text _timeText;


        public Action<int> OnClickRemoveOrderButtonHandler { get; set; }
        public Action<int> OnClickOpenRecipeBookButtonHandler { get; set; }


        public override void Initialize(int slotIndex, OrderStorageType storageType, bool isDragAndDropOn)
        {
            base.Initialize(slotIndex, storageType, false);
            _removeOrderButton.onClick.AddListener(OnClickRemoveOrderButton);
            _openRecipeBookButton.onClick.AddListener(OnClickOpenRecipeBookButton);
            _removeOrderButton.gameObject.SetActive(false);
            _processImage.SetActive(false);
        }

        public void FillSlot(Sprite sprite, bool? isOrderComplete = null)
        {
            base.FillSlot(sprite);
            _openRecipeBookButton.interactable = sprite == null;

            if (isOrderComplete.HasValue)
            {
                _removeOrderButton.gameObject.SetActive(!isOrderComplete.Value);
                _processImage.SetActive(!isOrderComplete.Value);
            }
            else
            {
                _removeOrderButton.gameObject.SetActive(false);
                _processImage.SetActive(false);
            }
        }

        public void UpdateCraftTimeText(int hours)
        {
            _timeText.text = $"{hours} {HubUIServices.SharedInstance.TimeService.GetHoursWord(hours)}";
            _processImage.SetActive(hours > 0);
        }

        private void OnClickRemoveOrderButton()
        {
            OnClickRemoveOrderButtonHandler?.Invoke(_slotIndex);
        }

        private void OnClickOpenRecipeBookButton()
        {
            OnClickOpenRecipeBookButtonHandler?.Invoke(_slotIndex);
        }
    }
}
