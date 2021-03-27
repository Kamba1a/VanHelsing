//using System.Collections.Generic;


//namespace BeastHunter
//{
//    public class HubMapUIDialogsController
//    {
//        #region Fields

//        //private Dictionary<HubMapUICitizenData, int> _currentDialogsIds;
//        private List<HubMapUIDialogNode> _dialogs;

//        #endregion


//        #region ClassLifeCycle

//        public HubMapUIDialogsController()
//        {
//            //_currentDialogsIds = new Dictionary<HubMapUICitizenData, int>();
//            _dialogs = new List<HubMapUIDialogNode>();
//            for (int i = 0; i < Data.HubMapData.Dialogs.Count; i++)
//            {
//                _dialogs.Add(Data.HubMapData.Dialogs[i]);
//            }
//        }

//        #endregion


//        #region Methods

//        //public void AddCitizen(HubMapUICitizenData citizen)
//        //{
//        //    if (!_currentDialogsIds.ContainsKey(citizen))
//        //    {
//        //        _currentDialogsIds.Add(citizen, citizen.FirstDialogId);
//        //    }
//        //}

//        //public int GetCurrentDialogId(HubMapUICitizenData citizen)
//        //{
//        //    if (_currentDialogsIds.ContainsKey(citizen))
//        //    {
//        //        return _currentDialogsIds[citizen];
//        //    }
//        //    else
//        //    {
//        //        throw new System.Exception(this + " does not contain the requested citizen");
//        //    }
//        //}

//        public void SetNewDialog(HubMapUICitizenModel citizen, int newDialogId)
//        {
//            HubMapUIDialogNode dialog = _dialogs.Find(dialog => dialog.Id == newDialogId);

//            citizen.CurrentDialogId = newDialogId;
//            citizen.CurrentSpeechText = dialog.Text;
//            citizen.CurrentExpectedResponses.Clear();
//            for (int i = 0; i < dialog.Answers.Length; i++)
//            {
//                citizen.CurrentExpectedResponses.Add(dialog.Answers[i]);
//            }

//            HubMapUIDialogAnswer additionalQuestAnswer = Data.HubMapData.QuestService.GetAdditionalQuestAnswer(newDialogId);
//            if (additionalQuestAnswer != null)
//            {
//                citizen.CurrentExpectedResponses.Add(additionalQuestAnswer);
//            }
//        }

//        #endregion
//    }
//}
