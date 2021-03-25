using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapUIFraction", order = 0)]
    public class HubMapUIFractionData: ScriptableObject
    {
        private string _name;
        private Sprite _logo;

        public string Name => _name;
        public Sprite Logo => _logo;
    }
}
