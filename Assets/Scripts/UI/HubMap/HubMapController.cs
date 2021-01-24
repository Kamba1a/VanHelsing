namespace BeastHunter
{
    public class HubMapController : IAwake, IUpdate, ITearDown
    {
        #region Fields

        GameContext _context;

        #endregion

        #region ClassLifeCycles

        public HubMapController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {

        }

        #endregion


        #region IUpdate

        public void Updating()
        {
            Data.HubMapData.Updating();
        }

        #endregion


        #region ITearDown

        public void TearDown()
        {

        }

        #endregion
    }
}

