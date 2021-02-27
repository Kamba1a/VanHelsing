using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCharacterModel : ICharacter
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private int?[] _itemsId;

        #endregion


        #region Properties

        public int Id => _id;
        public Sprite Portrait => _portrait;
        public int?[] ItemsId => _itemsId;

        #endregion
    }
}
