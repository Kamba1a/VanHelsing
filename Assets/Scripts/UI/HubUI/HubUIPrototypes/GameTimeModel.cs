using System;
using System.Collections;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class GameTimeModel
    {
        #region Constants

        private const float SKIP_TIME_DELAY = 1.0f;

        #endregion


        #region Properties

        public Action<GameTimeStruct> OnChangeTimeHandler { get; set; }

        public int HoursAmountPerDay { get; private set; }
        public GameTimeStruct CurrentTime { get; private set; }
        public bool IsTimePassing { get; private set; }

        #endregion


        #region ClassLifeCycle

        public GameTimeModel(HubUITimeSettingsStruct settings)
        {
            IsTimePassing = false;
            HoursAmountPerDay = settings.HoursAmountPerDay;
            CurrentTime = new GameTimeStruct(settings.DayOnStartGame, settings.HoursOnStartGame);
        }

        public GameTimeModel(int hoursAmountPerDay, int currentDay, int currentHour)
        {
            IsTimePassing = false;
            HoursAmountPerDay = hoursAmountPerDay;
            CurrentTime = new GameTimeStruct(currentDay, currentHour);
        }

        public GameTimeModel(int hoursAmountPerDay, GameTimeStruct currentTime)
        {
            IsTimePassing = false;
            HoursAmountPerDay = hoursAmountPerDay;
            CurrentTime = currentTime;
        }

        #endregion


        #region Methods

        public IEnumerator StartTimeSkip()
        {
            IsTimePassing = true;
            while (IsTimePassing)
            {
                yield return new WaitForSeconds(SKIP_TIME_DELAY);
                if (!IsTimePassing)
                {
                    yield break;
                }
               OneHourPass();
            }
        }

        public void StopTimeSkip()
        {
            IsTimePassing = false;
        }

        public void OneHourPass()
        {
            CurrentTime = AddOneHour(CurrentTime);
            OnChangeTime();
        }

        public GameTimeStruct AddTime(GameTimeStruct time)
        {
            return AddTime(GameTimeStructToHours(time));
        }


        public GameTimeStruct AddTime(int hours)
        {
            GameTimeStruct newTime = CurrentTime;

            if (hours > 0)
            {
                if (hours < HoursAmountPerDay - CurrentTime.Hour)
                {
                    newTime = new GameTimeStruct(newTime.Day, newTime.Hour + hours);
                }
                else
                {
                    int h = hours;
                    int d = 0;
                    while (h > HoursAmountPerDay - newTime.Hour)
                    {
                        h -= HoursAmountPerDay;
                        d++;
                    }
                    newTime = new GameTimeStruct(newTime.Day + d, newTime.Hour + h);
                }
            }

            return newTime;
        }

        private GameTimeStruct AddOneHour(GameTimeStruct currentTime)
        {
            if (currentTime.Hour + 1 >= HoursAmountPerDay)
            {
                return new GameTimeStruct(currentTime.Day + 1, 0);
            }
            else
            {
                return new GameTimeStruct(currentTime.Day, currentTime.Hour + 1);
            }
        }

        private int GameTimeStructToHours(GameTimeStruct time)
        {
            return time.Hour + time.Day * HoursAmountPerDay;
        }

        private void OnChangeTime()
        {
            OnChangeTimeHandler?.Invoke(CurrentTime);
        }

        #endregion
    }
}
