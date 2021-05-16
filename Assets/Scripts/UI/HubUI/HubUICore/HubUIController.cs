﻿using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    class HubUIController : MonoBehaviour
    {
        #region Fields

        [SerializeField] GameObject _messagePanel;

        private HubUIContext _context;
        private HubUIData _data;

        private List<IStart> _startBehaviours;
        private List<IUpdate> _updateBehaviours;
        private List<IDestroy> _destroyBehaviours;

        private MapBehaviour _mapBehaviour;
        private QuestRoomUIBehaviour _questRoomUIBehaviour;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _startBehaviours = new List<IStart>();
            _updateBehaviours = new List<IUpdate>();
            _destroyBehaviours = new List<IDestroy>();

            _mapBehaviour = GetComponentInChildren<MapBehaviour>(true);
            _questRoomUIBehaviour = GetComponentInChildren<QuestRoomUIBehaviour>(true);

            Add(_mapBehaviour);
            Add(_questRoomUIBehaviour);
        }

        private void Start()
        {
            _data = BeastHunter.Data.HubUIData;
            _context = new HubUIContext();

            HubUIServices.SharedInstance.InitializeServices(_context);
            _data.AllCharactersData.InitializeCharacter3DViewModelRendering();
            _context.InitializeGameContent(_data);
            new QuestController(_data.QuestsPool, _context);
            Subscriptions();

            StartBehaviours(_context);

            _messagePanel.SetActive(false);
        }

        private void Update()
        {
            for (int i = 0; i < _updateBehaviours.Count; i++)
            {
                _updateBehaviours[i].Updating();
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _destroyBehaviours.Count; i++)
            {
                _destroyBehaviours[i].Destroying();
            }

            Unsubscribes();
            HubUIServices.SharedInstance.DisposeGameServices();
        }

        #endregion


        #region Methods

        private void Subscriptions()
        {
            _context.GameTime.OnChangeTimeHandler += HubUIServices.SharedInstance.EventsService.OnChangedGameTime;
            HubUIServices.SharedInstance.GameMessages.OnWindowMessageHandler += InitializeMessageWindow;
        }

        private void Unsubscribes()
        {
            _context.GameTime.OnChangeTimeHandler -= HubUIServices.SharedInstance.EventsService.OnChangedGameTime;
            HubUIServices.SharedInstance.GameMessages.OnWindowMessageHandler -= InitializeMessageWindow;
        }

        private void StartBehaviours(HubUIContext context)
        {
            for (int i = 0; i < _startBehaviours.Count; i++)
            {
                _startBehaviours[i].Starting(context);
            }
        }

        private void Add(MonoBehaviour behaviour)
        {
            if (behaviour is IStart startBehaviour)
            {
                _startBehaviours.Add(startBehaviour);
            }

            if (behaviour is IUpdate updateBehaviour)
            {
                _updateBehaviours.Add(updateBehaviour);
            }

            if (behaviour is IDestroy destroyBehaviour)
            {
                _destroyBehaviours.Add(destroyBehaviour);
            }
        }

        private void InitializeMessageWindow(string message)
        {
            GameObject messageUI = InstantiateUIObject(_data.MessageWindowPrefab, _messagePanel);
            MapMessageWindowBehaviour behaviour = messageUI.GetComponent<MapMessageWindowBehaviour>();
            behaviour.OnCloseWindowHandler += OnClose_MessageWindow;
            behaviour.FillInfo(message);

            if (!_messagePanel.activeSelf)
            {
                _messagePanel.SetActive(true);
            }
        }

        private void OnClose_MessageWindow()
        {
            if (_messagePanel.transform.GetComponentsInChildren<MapMessageWindowBehaviour>(false).Length == 0)
            {
                _messagePanel.SetActive(false);
            }
        }

        private GameObject InstantiateUIObject(GameObject prefab, GameObject parent)
        {
            GameObject objectUI = GameObject.Instantiate(prefab);
            objectUI.transform.SetParent(parent.transform, false);
            objectUI.transform.localScale = new Vector3(1, 1, 1);
            return objectUI;
        }

        #endregion
    }
}
