using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    class HubUIController : MonoBehaviour
    {
        #region Fields

        HubUIContext _context;

        private List<IStart> _startBehaviours;
        private List<IUpdate> _updateBehaviours;
        private List<IDestroy> _destroyBehaviours;

        private MapBehaviour _mapBehaviour;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _startBehaviours = new List<IStart>();
            _updateBehaviours = new List<IUpdate>();
            _destroyBehaviours = new List<IDestroy>();

            _mapBehaviour = GetComponentInChildren<MapBehaviour>();
            Add(_mapBehaviour);
        }

        private void Start()
        {
            _context = new HubUIContext();
            HubUIServices.SharedInstance.InitializeServices(_context);
            _context.Initialize(BeastHunter.Data.HubUIData.ContextDataStruct);
            _context.GameTime.OnChangeTimeHandler += HubUIServices.SharedInstance.EventsService.OnChangedGameTime;
            new QuestController(_context);
            StartBehaviours(_context);
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

            _context.GameTime.OnChangeTimeHandler -= HubUIServices.SharedInstance.EventsService.OnChangedGameTime;
            HubUIServices.SharedInstance.DisposeGameServices();
        }

        #endregion


        #region Methods

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

        #endregion
    }
}
