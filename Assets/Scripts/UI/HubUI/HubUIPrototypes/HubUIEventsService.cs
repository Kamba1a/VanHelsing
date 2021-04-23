using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class HubUIEventsService
    {
        private const float RANDOM_EVENT_CHANCE = 0.05f; 


        private GameTimeModel _gameTimeModel;
        private Dictionary<GameTimeStruct, List<HubUIEventModel>> _scheduledEvents;


        public HubUIEventsService(GameTimeModel gameTimeModel)
        {
            _gameTimeModel = gameTimeModel;
            _gameTimeModel.OnChangeTimeHandler += OnChangedGameTime;

            _scheduledEvents = new Dictionary<GameTimeStruct, List<HubUIEventModel>>();
        }


        public void AddEventToScheduler(HubUIEventModel eventModel)
        {
            GameTimeStruct invokeTime = eventModel.InvokeTime;
            if (!_scheduledEvents.ContainsKey(invokeTime))
            {
                _scheduledEvents.Add(invokeTime, new List<HubUIEventModel>());
            }
            _scheduledEvents[invokeTime].Add(eventModel);
        }

        private void OnChangedGameTime(GameTimeStruct currentTime)
        {
            RandomEventCheck();
            ScheduleEventsCheck(currentTime);
        }

        private void ScheduleEventsCheck(GameTimeStruct invokeTime)
        {
            if (_scheduledEvents.ContainsKey(invokeTime))
            {
                //todo: stop time skip
                for (int i = 0; i < _scheduledEvents[invokeTime].Count; i++)
                {
                    _scheduledEvents[invokeTime][i].Invoke();
                    HubUIServices.SharedInstance.GameMessages.Window($"Event {_scheduledEvents[invokeTime][i].Name} has happened!");
                }
            }
        }

        private void RandomEventCheck() 
        { 
            if (Random.Range(0, 101) <= RANDOM_EVENT_CHANCE * 100)
            {
                //todo: !!a random event must stopped time skip!!
                HubUIServices.SharedInstance.GameMessages.Window("A random event has happened!");
            }
        }
    }
}
