namespace BeastHunterHubUI
{
    public class TimeService
    {
        private HubUIContext _context;


        public TimeService(HubUIContext context)
        {
            _context = context;
        }


        public HubUITimeStruct CalculateTime(int addedHours)
        {
            return _context.GameTime.AddTime(addedHours);
        }
    }
}
