using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public abstract class HubMapUIBaseEntity
    {
        #region Fields

        [SerializeField] [ReadOnlyInUnityInspector] private int _id;

        #endregion


        #region Properties

        public int Id => _id;

        #endregion


        #region Methods

        public void SetId(int id)
        {
            _id = id;
        }

        #endregion
    }
}
