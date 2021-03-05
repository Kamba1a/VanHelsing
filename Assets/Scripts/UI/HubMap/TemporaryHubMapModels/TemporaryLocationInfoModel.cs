using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryLocationInfoModel : ILocationInfo
    {
        #region Fields

        private int _id;
        [SerializeField] private string _name;
        [SerializeField][TextArea(3, 10)] private string _description;
        [SerializeField] private Sprite _screenshot;
        [SerializeField] private int[] _dwellersId;
        [SerializeField] private int[] _ingredientsId;

        #endregion


        #region Properties

        public int Id => _id;
        public string Name => _name;
        public string Description => _description;
        public Sprite Screenshot => _screenshot;
        public int[] DwellersId => _dwellersId;
        public int[] IngredientsId => _ingredientsId;

        #endregion


        #region Methods

        public void SetId(int id)
        {
            _id = id;
        }

        #endregion
    }
}
