using System;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomButtonBehaviour : MonoBehaviour
    {
        [SerializeField] Text _roomNameText;
        [SerializeField] Text _roomTimeText;


        public Action<WorkRoomModel> OnClickButtonHandler { get; set; }


        public void FillUIInfo(WorkRoomModel room)
        {
            _roomNameText.text = room.Name;
            int time = room.GetMinOrderCompleteTime();
            _roomTimeText.text = $"({time} {HubUIServices.SharedInstance.TimeService.GetHoursWord(time)})";
            GetComponent<Button>().onClick.AddListener(() => OnClickButton(room));
        }


        private void OnClickButton(WorkRoomModel room)
        {
            OnClickButtonHandler?.Invoke(room);
        }
    }
}
