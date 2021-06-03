using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class HubUIContext
    {
        #region Properties

        public PlayerModel Player { get; private set; }
        public List<CharacterModel> CharactersForHire { get; private set; }
        public List<LocationModel> Locations { get; private set; }
        public List<CityModel> Cities { get; private set; }
        public GameTimeModel GameTime { get; private set; }
        public List<WorkRoomModel> WorkRooms { get; private set; }

        #endregion


        #region ClassLifeCycle

        public HubUIContext()
        {
            CharactersForHire = new List<CharacterModel>();
            Cities = new List<CityModel>();
            Locations = new List<LocationModel>();
            WorkRooms = new List<WorkRoomModel>();
        }

        #endregion


        #region Methods

        public void InitializeGameContent(GameDataStruct gameData)
        {
            HubUIData hubUIData = BeastHunter.Data.HubUIData;

            Player = new PlayerModel(gameData.PlayerStruct);
            GameTime = new GameTimeModel(gameData.GameTimeStruct);

            CharactersForHire = new List<CharacterModel>();
            for (int i = 0; i < hubUIData.CharactersAmountForHire; i++)
            {
                CharacterModel newCharacter = new CharacterModel(Player.Rank);
                CharactersForHire.Add(newCharacter);

                Player.HireCharacter(newCharacter); //todo: remove that line after realize characters hire! (for debug only)
            }

            for (int i = 0; i < gameData.CitiesData.Length; i++)
            {
                Cities.Add(new CityModel(gameData.CitiesData[i]));
            }

            for (int i = 0; i < gameData.LocationsData.Length; i++)
            {
                Locations.Add(new LocationModel(gameData.LocationsData[i]));
            }

            for (int i = 0; i < gameData.WorkRoomsData.Length; i++)
            {
                WorkRooms.Add(new WorkRoomModel(gameData.WorkRoomsData[i].WorkRoomStruct));
            }
        }

        //TODO: REDONE
        public MapObjectModel GetMapObjectModel(MapObjectData mapObjectData)
        {
            switch (mapObjectData.GetMapObjectType())
            {
                case MapObjectType.Location:
                    return GetLocation(mapObjectData as LocationData);

                case MapObjectType.City:
                    return GetCity(mapObjectData as CityData);

                default:
                    return null;
            }
        }

        public CityModel GetCity(CityData cityData)
        {
            if (cityData != null)
            {
                return Cities.Find(city => city.DataInstanceID == cityData.GetInstanceID());
            }
            else
            {
                Debug.LogError(this + ": input parameter is null");
                return null;
            }
        }

        public LocationModel GetLocation(LocationData locationData)
        {
            if (locationData != null)
            {
                return Locations.Find(location => location.DataInstanceID == locationData.GetInstanceID());
            }
            else
            {
                Debug.LogError(this + ": input parameter is null");
                return null;
            }
        }

        public CitizenModel GetCitizen(CitizenData citizenData)
        {
            if (citizenData != null)
            {
                int citizenDataInstanceID = citizenData.GetInstanceID();
                for (int cityIndex = 0; cityIndex < Cities.Count; cityIndex++)
                {
                    for (int citizenIndex = 0; citizenIndex < Cities[cityIndex].Citizens.Count; citizenIndex++)
                    {
                        if (Cities[cityIndex].Citizens[citizenIndex].DataInstanceId == citizenDataInstanceID)
                        {
                            return Cities[cityIndex].Citizens[citizenIndex];
                        }
                    }
                }
                return null;
            }
            else
            {
                Debug.LogError(this + ": input parameter is null");
                return null;
            }
        }

        #endregion
    }
}
