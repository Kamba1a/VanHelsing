using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class ItemOrderModel
    {
        #region Fields

        protected HubUIEventModel _orderEvent;
        private float _progressPerTick;
        private int _hoursNumberToComplete;

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
            UpdateHoursNumberToComplete(timeReducePercent);
        }

        #endregion


        #region Methods

        public void AddOrderEvent()
        {
            _orderEvent = new HubUIEventModel(HoursNumberToComplete, true);
            _orderEvent.OnInvokeHandler = Complete;
            _orderEvent.OnTickTimeHandler += TimeTick;
            HubUIServices.SharedInstance.EventsService.AddEventToScheduler(_orderEvent);
        }

        public void RemoveOrderEvent()
        {
            if (_orderEvent != null)
            {
                _orderEvent.OnInvokeHandler = null;
                _orderEvent.OnTickTimeHandler = null;
                HubUIServices.SharedInstance.EventsService.RemoveEventFromScheduler(_orderEvent);
                _orderEvent = null;
            }
        }

        public void UpdateHoursNumberToComplete(float timeReducePercent)
        {
            Debug.Log("timeReducePercent " + timeReducePercent);
            float baseHoursToComplete = Recipe.BaseHoursNumberToComplete * timeReducePercent;
            Debug.Log("baseHoursToComplete " + baseHoursToComplete);
            HoursNumberToComplete = (int)Mathf.Round(baseHoursToComplete * ProgressToComplete);
            Debug.Log("HoursNumberToComplete " + HoursNumberToComplete);
            _progressPerTick = (100 / HoursNumberToComplete) / 100;
        }

        public void TimeTick()
        {
            int spentHour = 1;
            HoursNumberToComplete = -spentHour;
            Mathf.Clamp(ProgressToComplete += _progressPerTick * spentHour, 0, 1);
        }

        private void Complete()
        {
            //todo: add fail chance
            IsCompleted = true;
            MakedItem = HubUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(Recipe.Item);
            OnCompleteHandler?.Invoke(this);
        }

        #endregion
    }
}
