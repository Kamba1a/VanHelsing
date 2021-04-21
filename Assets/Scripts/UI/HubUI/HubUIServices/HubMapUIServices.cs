namespace BeastHunter
{
    public class HubMapUIServices
    {
        public static readonly HubMapUIServices SharedInstance = new HubMapUIServices();


        public HubMapUIShopService ShopService { get; private set; }
        public HubMapUITravelTimeService TravelTimeService { get; private set; }
        public HubMapUIItemInitializeService ItemInitializeService { get; private set; }
        public MainInput MainInput { get; private set; }
        public HubMapUIGameMessages GameMessages { get;private set;}


        public void InitializeServices(HubMapUIContext context)
        {
            ShopService = new HubMapUIShopService();
            TravelTimeService = new HubMapUITravelTimeService();
            ItemInitializeService = new HubMapUIItemInitializeService();
            GameMessages = new HubMapUIGameMessages();

            MainInput = new MainInput();
            MainInput.Enable();
        }

        public void DisposeGameServices()
        {
            MainInput.Disable();
        }
    }
}
