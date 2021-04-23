using System;


namespace BeastHunterHubUI
{
    public class HubUIEventModel
    {
        #region Properties

        public Action OnInvokeHandler { get; private set; }

        public string Name { get; private set; }
        public GameTimeStruct InvokeTime { get; private set; }

        #endregion


        #region ClassLifeCycle

        public HubUIEventModel(string name, GameTimeStruct invokeTime)
        {
            Name = name;
            InvokeTime = invokeTime;
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
