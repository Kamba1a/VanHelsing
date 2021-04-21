using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    class HubMapUIGameController : MonoBehaviour
    {
        private HubMapUIContext _context;
        private List<IHubMapUIStart> _startBehaviours;
        private List<IHubMapUIUpdate> _updateBehaviours;
        private List<IHubMapUIDestroy> _destroyBehaviours;


        private void Awake()
        {
            _context = new HubMapUIContext();
            _startBehaviours = new List<IHubMapUIStart>();
            _updateBehaviours = new List<IHubMapUIUpdate>();
            _destroyBehaviours = new List<IHubMapUIDestroy>();
        }

        private void Start()
        {
            HubMapUIServices.SharedInstance.InitializeServices(_context);
            new HubMapUIGameContentInitializeController(_context);
            new HubMapUIQuestController(_context);

            Add(GameObject.Find("HubMap").GetComponent<HubMapUIMainBehaviour>());
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

            HubMapUIServices.SharedInstance.DisposeGameServices();
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
            if (behaviour is IHubMapUIStart startBehaviour)
            {
                _startBehaviours.Add(startBehaviour);
            }

            if (behaviour is IHubMapUIUpdate updateBehaviour)
            {
                _updateBehaviours.Add(updateBehaviour);
            }

            if (behaviour is IHubMapUIDestroy destroyBehaviour)
            {
                _destroyBehaviours.Add(destroyBehaviour);
            }
        }
    }
}
