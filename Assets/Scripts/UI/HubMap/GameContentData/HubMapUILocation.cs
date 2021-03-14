using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUILocation", menuName = "CreateData/HubMapUIData/HubMapUILocation", order = 0)]
    public class HubMapUILocation : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField][TextArea(3, 10)] private string _description;
        [SerializeField] private Sprite _screenshot;
        [SerializeField] private HubMapUIDweller[] _dwellers;
        [SerializeField] private HubMapUIIngredient[] _ingredients;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public Sprite Screenshot => _screenshot;
        public HubMapUIDweller[] Dwellers => _dwellers;
        public HubMapUIIngredient[] Ingredients => _ingredients;

        #endregion
    }
}
