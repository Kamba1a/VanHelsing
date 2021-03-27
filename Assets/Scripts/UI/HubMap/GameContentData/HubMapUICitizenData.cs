using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICitizen", menuName = "CreateData/HubMapUIData/HubMapUICitizen", order = 0)]
    public class HubMapUICitizenData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private int _firstDialogId;
        [SerializeField] private List<HubMapUIDialogNode> _dialogs;
        [SerializeField] private List<HubMapUIQuestAnswer> _questAnswers;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public int FirstDialogId => _firstDialogId;
        public List<HubMapUIDialogNode> Dialogs => _dialogs;
        public List<HubMapUIQuestAnswer> QuestAnswers => _questAnswers;

        #endregion
    }
}
