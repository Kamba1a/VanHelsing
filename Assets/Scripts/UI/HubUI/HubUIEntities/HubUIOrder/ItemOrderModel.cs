using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class ItemOrderModel
    {
        #region Fields

        protected HubUIEventModel _orderEvent;
        private int _hoursNumberToComplete;
        private int _baseHoursToComplete;

        #endregion


        #region Properties

        public Action<ItemOrderModel> OnCompleteHandler { get; set; }
        public Action<int> OnChangeHoursNumberToCompleteHandler { get; set; }

        public bool IsCompleted { get; private set; }
        public float ProgressToComplete { get; private set; }
        public ItemRecipeData Recipe { get; private set; }
        public BaseItemModel MakedItem { get; private set; }
        public int HoursNumberToComplete
        {
            get => _hoursNumberToComplete;
            private set
            {
                if (value != _hoursNumberToComplete)
                {
                    _hoursNumberToComplete = value;
                    OnChangeHoursNumberToCompleteHandler?.Invoke(_hoursNumberToComplete);
                }
            }
        }

        #endregion


        #region ClassLifeCycle

        public ItemOrderModel(ItemRecipeData recipe, float timeReducePercent)
        {
            IsCompleted = false;
            Recipe = recipe;
            ProgressToComplete = 0.0f;
            RecountHoursToComplete(timeReducePercent);
        }

        #endregion


        #region Methods

        public void AddOrderEvent()
        {
            _orderEvent = new HubUIEventModel(HoursNumberToComplete, true);
            _orderEvent.OnInvokeHandler = Complete;
            _orderEvent.OnTickTimeHandler += TimeTickUpdate;
            HubUIServices.SharedInstance.EventsService.AddEventToScheduler(_orderEvent);
        }

        public void RemoveOrderEvent()
        {
            if (_orderEvent != null)
            {
                HubUIServices.SharedInstance.EventsService.RemoveEventFromScheduler(_orderEvent);
                _orderEvent.OnInvokeHandler = null;
                _orderEvent.OnTickTimeHandler = null;
                _orderEvent = null;
            }
        }

        public void RecountHoursToComplete(float timeReducePercent)
        {
            Debug.Log("UpdateHoursNumberToComplete()");
            Debug.Log("timeReducePercent=" + timeReducePercent);

            _baseHoursToComplete = (int)Mathf.Round(Recipe.BaseHoursNumberToComplete * timeReducePercent);
            HoursNumberToComplete = (int)Mathf.Round(_baseHoursToComplete - (_baseHoursToComplete * ProgressToComplete));

            if(_orderEvent != null)
            {
                RemoveOrderEvent();
                AddOrderEvent();
            }

            Debug.Log("_baseHoursToComplete=" + _baseHoursToComplete);
            Debug.Log("HoursNumberToComplete=" + HoursNumberToComplete);
        }

        private void TimeTickUpdate()
        {
            Debug.Log("TimeTickUpdate()");

            int spentHour = 1;
            HoursNumberToComplete = HoursNumberToComplete - spentHour < 0 ? 0 : HoursNumberToComplete - spentHour;
            ProgressToComplete = (float)(_baseHoursToComplete - HoursNumberToComplete) / _baseHoursToComplete;

            Debug.Log("HoursNumberToComplete="+ HoursNumberToComplete);
            Debug.Log("ProgressToComplete=" + ProgressToComplete);
        }

        private void Complete()
        {
            //todo: add fail chance
            IsCompleted = true;

            _orderEvent.OnInvokeHandler = null;
            _orderEvent.OnTickTimeHandler = null;
            _orderEvent = null;

            MakedItem = HubUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(Recipe.Item);
            OnCompleteHandler?.Invoke(this);
        }

        #endregion
    }
}
