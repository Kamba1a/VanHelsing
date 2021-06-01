//using UnityEngine;
//using UnityEngine.UI;


//namespace BeastHunterHubUI
//{
//    class WorkRoomMakedItemSlotBehaviour : BaseSlotBehaviour<ItemStorageType>
//    {
//        [SerializeField] private GameObject _processImage;
//        [SerializeField] private Text _timeText;


//        public override void FillSlot(Sprite sprite)
//        {
//            base.FillSlot(sprite);
//            if(sprite == null)
//            {
//                _processImage.SetActive(false);
//            }
//        }

//        public void UpdateTimeText(int hours)
//        {
//            _timeText.text = $"{hours} {HubUIServices.SharedInstance.TimeService.GetHoursWord(hours)}";
//            _processImage.SetActive(hours > 0);
//        }
//    }
//}
