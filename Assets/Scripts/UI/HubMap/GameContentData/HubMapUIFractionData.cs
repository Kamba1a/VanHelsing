using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIFraction", menuName = "CreateData/HubMapUIData/HubMapUIFraction", order = 0)]
    public class HubMapUIFractionData: ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _logo;

        public string Name => _name;
        public Sprite Logo => _logo;
    }
}
