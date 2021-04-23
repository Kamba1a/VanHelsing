using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunterHubUI
{
    public class GameTimeModel
    {
        #region Properties

        public Action<GameTimeStruct> OnChangeTimeHandler;

        public int HoursAmountPerDay { get; private set; }
        public GameTimeStruct CurrentTime { get; private set; }

        #endregion


        #region ClassLifeCycle

        public GameTimeModel(int hoursAmountPerDay, int currentDay, int currentHour)
        {
            HoursAmountPerDay = hoursAmountPerDay;
            CurrentTime = new GameTimeStruct(currentDay, currentHour);
        }

        public GameTimeModel(int hoursAmountPerDay, GameTimeStruct currentTime)
        {
            HoursAmountPerDay = hoursAmountPerDay;
            CurrentTime = currentTime;
        }

        #endregion


        #region Methods

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
                //for (int i = 0; i < hours; i++)
                //{
                //    newTime = AddOneHour(newTime);
                //}

                if (hours < HoursAmountPerDay - CurrentTime.Hour)
                {
                    newTime = new GameTimeStruct(newTime.Day, newTime.Hour + hours);
                }
                else
                {
                    int h = hours;
                    int d = 0;
                    while (h > HoursAmountPerDay - 1)
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
