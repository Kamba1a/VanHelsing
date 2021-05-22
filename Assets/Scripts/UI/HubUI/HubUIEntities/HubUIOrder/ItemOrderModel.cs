using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class ItemOrderModel
    {
        #region Fields

        protected HubUIEventModel _orderEvent;
        private float _progressPerTick;

        #endregion


        #region Properties

        public Action<ItemOrderModel> OnCompleteHandler { get; set; }

        public int HoursNumberToComplete { get; private set; }
        public float ProgressToComplete { get; private set; }
        public ItemRecipeData Recipe { get; private set; }
        public BaseItemModel MakedItem { get; private set; }

        #endregion


        #region ClassLifeCycle

        public ItemOrderModel(ItemRecipeData recipe, float timeReducePercent)
        {
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
            float baseHoursToComplete = Recipe.BaseHoursNumberToComplete * timeReducePercent;
            HoursNumberToComplete = (int)Mathf.Round(baseHoursToComplete * ProgressToComplete);
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
            MakedItem = HubUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(Recipe.Item);
            OnCompleteHandler?.Invoke(this);
        }

        #endregion
    }
}
