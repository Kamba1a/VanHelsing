using UnityEngine;

namespace BeastHunter
{
    class PlayerHealthBarInitializeController : IAwake
    {
        #region Fields

        private GameContext _context;

        #endregion


        #region ClassLifeCycles

        public PlayerHealthBarInitializeController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            GameObject playerHealthBar = GameObject.Instantiate(Data.PlayerHealthBarData.PlayerHealthBarPrefab);
            _context.PlayerHealthBarModel = new PlayerHealthBarModel(playerHealthBar, Data.PlayerHealthBarData);
        }

        #endregion
    }
}
