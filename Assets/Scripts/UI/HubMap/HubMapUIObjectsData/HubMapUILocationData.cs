using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUILocation", menuName = "CreateData/HubMapUIData/HubMapUILocation", order = 0)]
    public class HubMapUILocationData : ScriptableObject
    {
        #region Fields

        [SerializeField] private int _loadSceneId;
        [SerializeField] private string _name;
        [SerializeField][TextArea(3, 10)] private string _description;
        [SerializeField] private Sprite _screenshot;
        [SerializeField] private bool _isBlockedAtStart;
        [SerializeField] private HubMapUIDwellerData[] _dwellers;
        [SerializeField] private HubMapUIIngredientData[] _ingredients;


        #endregion


        #region Properties

        public int LoadSceneId => _loadSceneId;
        public string Name => _name;
        public string Description => _description;
        public Sprite Screenshot => _screenshot;
        public bool IsBlockedAtStart => _isBlockedAtStart;
        public HubMapUIDwellerData[] Dwellers => _dwellers;
        public HubMapUIIngredientData[] Ingredients => _ingredients;

        #endregion
    }
}
