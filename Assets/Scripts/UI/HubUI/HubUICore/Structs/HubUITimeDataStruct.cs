using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    [Serializable]
    public class HubUITimeSettingsStruct
    {
        [SerializeField] private int _hoursAmountPerDay;
        [SerializeField] private int _hoursOnStartGame;
        [SerializeField] private int _dayOnStartGame;

        public int HoursAmountPerDay => _hoursAmountPerDay;
        public int HoursOnStartGame => _hoursOnStartGame;
        public int DayOnStartGame => _dayOnStartGame;
    }
}
