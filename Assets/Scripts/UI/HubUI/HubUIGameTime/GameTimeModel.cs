using System;


namespace BeastHunterHubUI
{
    public class GameTimeModel
    {
        public int HoursAmountPerDay { get; private set; }
        public int CurrentDay { get; private set; }
        public int CurrentHour { get; private set; }


        public Action<int, int> OnChangeTimeHandler;


        public GameTimeModel(int hoursAmountPerDay, int currentDay, int currentHour)
        {
            HoursAmountPerDay = hoursAmountPerDay;
            CurrentDay = currentDay;
            CurrentHour = currentHour;
        }


        public void HoursPass(int hoursAmount)
        {
            if (hoursAmount > 0)
            {
                for (int i = 0; i < hoursAmount; i++)
                {
                    OneHourPass();
                }
            }
            OnChangeTime();
        }

        private void OneHourPass()
        {
            if (CurrentHour + 1 >= HoursAmountPerDay)
            {
                CurrentHour = 0;
                CurrentDay += 1;
            }
            else
            {
                CurrentHour += 1;
            }
        }

        private void OnChangeTime()
        {
            OnChangeTimeHandler?.Invoke(CurrentDay, CurrentHour);
        }
    }
}
