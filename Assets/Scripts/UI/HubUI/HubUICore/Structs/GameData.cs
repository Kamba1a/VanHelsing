using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    [Serializable]
    public class GameData
    {
        #region Fields

        [SerializeField] private GameTimeStruct _gameTimeStruct;
        [SerializeField] private PlayerStruct _playerStruct;
        [SerializeField] private LocationData[] _locationsData;
        [SerializeField] private CityData[] _citiesData;
        [SerializeField] private WorkRoomData[] _workRoomsData;

        #endregion


        #region Properties

        public GameTimeStruct GameTimeStruct => _gameTimeStruct;
        public PlayerStruct PlayerStruct => _playerStruct;
        public LocationData[] LocationsData => (LocationData[])_locationsData.Clone();
        public CityData[] CitiesData => (CityData[])_citiesData.Clone();
        public WorkRoomData[] WorkRoomsData => (WorkRoomData[])_workRoomsData.Clone();

        #endregion
    }
}
