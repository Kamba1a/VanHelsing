﻿using UnityEngine;

namespace BeastHunter
{
    class HubMapInitializeController: IAwake
    {
        #region Fields

        GameContext _context;

        #endregion


        #region ClassLifeCycle

        public HubMapInitializeController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        public void OnAwake()
        {
            //GameObject instance = GameObject.Instantiate(Data.HubMapData.HubMapPrefab);
        }

        #endregion
    }
}
