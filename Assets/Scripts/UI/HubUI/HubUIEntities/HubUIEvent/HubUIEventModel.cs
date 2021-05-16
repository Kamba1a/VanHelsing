using System;


namespace BeastHunterHubUI
{
    public class HubUIEventModel
    {
        #region Properties

        public Action OnInvokeHandler { get; set; }

        public HubUITimeStruct InvokeTime { get; private set; }
        public string Message { get; private set; }

        #endregion


        #region ClassLifeCycle

        public HubUIEventModel(int invokeHoursAmount, string invokeMessage)
        {
            InvokeTime = HubUIServices.SharedInstance.TimeService.CalculateTime(invokeHoursAmount);
            Message = invokeMessage;
        }

        #endregion


        #region Methods

        public void Invoke()
        {
            OnInvokeHandler?.Invoke();
        }

        #endregion
    }
}
