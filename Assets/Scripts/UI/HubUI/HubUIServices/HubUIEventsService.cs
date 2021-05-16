using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class HubUIEventsService
    {
        private const float RANDOM_EVENT_CHANCE = 0.05f; 


        private HubUIContext _context;
        private Dictionary<HubUITimeStruct, List<HubUIEventModel>> _scheduledEvents;


        public HubUIEventsService(HubUIContext context)
        {
            _context = context;
            _scheduledEvents = new Dictionary<HubUITimeStruct, List<HubUIEventModel>>();
        }


        public void RemoveEventFromScheduler(HubUIEventModel eventModel)
        {
            if (_scheduledEvents.ContainsKey(eventModel.InvokeTime))
            {
                _scheduledEvents[eventModel.InvokeTime].Remove(eventModel);
            }
        }

        public void AddEventToScheduler(HubUIEventModel eventModel)
        {
            if (!_scheduledEvents.ContainsKey(eventModel.InvokeTime))
            {
                _scheduledEvents.Add(eventModel.InvokeTime, new List<HubUIEventModel>());
            }
            _scheduledEvents[eventModel.InvokeTime].Add(eventModel);
        }

        public void OnChangedGameTime(HubUITimeStruct currentTime)
        {
            RandomEventCheck();
            ScheduleEventsCheck(currentTime);
        }

        private void ScheduleEventsCheck(HubUITimeStruct invokeTime)
        {
            if (_scheduledEvents.ContainsKey(invokeTime))
            {
                _context.GameTime.StopTimeSkip();
                for (int i = 0; i < _scheduledEvents[invokeTime].Count; i++)
                {
                    HubUIServices.SharedInstance.GameMessages.Window(_scheduledEvents[invokeTime][i].Message);
                    _scheduledEvents[invokeTime][i].Invoke();
                }
                _scheduledEvents.Remove(invokeTime);
            }
        }

        private void RandomEventCheck() 
        { 
            if (Random.Range(0, 101) <= RANDOM_EVENT_CHANCE * 100)
            {
                _context.GameTime.StopTimeSkip();
                HubUIServices.SharedInstance.GameMessages.Window("A random event has happened!");
            }
        }
    }
}
