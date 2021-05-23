using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    class WorkRoomUIBehaviour : MonoBehaviour, IStart
    {
        [SerializeField] private GameObject _roomButtonsFillablePanel;


        private HubUIContext _context;
        private HubUIData _data;


        public void Starting(HubUIContext context)
        {
            _context = context;
            _data = BeastHunter.Data.HubUIData;


            for (int i = 0; i < _context.WorkRooms.Count; i++)
            {
                InitializeWorkRoomButton(_context.WorkRooms[i]);
            }
        }


        private void InitializeWorkRoomButton(WorkRoomModel model)
        {
            GameObject buttonUI = InstantiateUIObject(_data.WorkRoomDataStruct.WorkRoomButtonPrefab, _roomButtonsFillablePanel);
            WorkRoomButtonBehaviour behaviour = buttonUI.GetComponentInChildren<WorkRoomButtonBehaviour>();
            behaviour.FillUIInfo(model);
            behaviour.OnClickButtonHandler += OnClick_RoomButton;
        }

        private void OnClick_RoomButton(WorkRoomModel model)
        {
            Debug.Log(model.Name);
        }

        private GameObject InstantiateUIObject(GameObject prefab, GameObject parent)
        {
            GameObject objectUI = GameObject.Instantiate(prefab);
            objectUI.transform.SetParent(parent.transform, false);
            objectUI.transform.localScale = new Vector3(1, 1, 1);
            return objectUI;
        }

    }
}
