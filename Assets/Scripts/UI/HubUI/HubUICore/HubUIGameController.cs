using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    class HubUIGameController : MonoBehaviour
    {
        private HubUIContext _context;
        private List<IStart> _startBehaviours;
        private List<IUpdate> _updateBehaviours;
        private List<IDestroy> _destroyBehaviours;


        private void Awake()
        {
            _context = new HubUIContext();
            _startBehaviours = new List<IStart>();
            _updateBehaviours = new List<IUpdate>();
            _destroyBehaviours = new List<IDestroy>();
        }

        private void Start()
        {
            HubUIServices.SharedInstance.InitializeServices(_context);
            new HubUIGameContentInitializeController(_context);
            new QuestController(_context);

            Add(GameObject.Find("HubUI").GetComponentInChildren<MapBehaviour>());
            Initialize();
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

            HubUIServices.SharedInstance.DisposeGameServices();
        }

        private void Initialize()
        {
            for (int i = 0; i < _startBehaviours.Count; i++)
            {
                _startBehaviours[i].Starting(_context);
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
    }
}
