using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIDialogsController
    {
        #region Fields

        private Dictionary<HubMapUICitizen, int> _currentDialogsIds;

        #endregion


        #region ClassLifeCycle

        public HubMapUIDialogsController()
        {
            _currentDialogsIds = new Dictionary<HubMapUICitizen, int>();
        }

        #endregion


        #region Methods

        public void AddCitizen(HubMapUICitizen citizen)
        {
            if (!_currentDialogsIds.ContainsKey(citizen))
            {
                _currentDialogsIds.Add(citizen, citizen.FirstDialogId);
            }
        }

        public int GetCurrentDialogId(HubMapUICitizen citizen)
        {
            if (_currentDialogsIds.ContainsKey(citizen))
            {
                return _currentDialogsIds[citizen];
            }
            else
            {
                throw new System.Exception(this + " does not contain the requested citizen");
            }
        }

        public void SetNewDialogId(HubMapUICitizen citizen, int id)
        {
            if (_currentDialogsIds.ContainsKey(citizen))
            {
                _currentDialogsIds[citizen] = id;
            }
            else
            {
                _currentDialogsIds.Add(citizen, id);
            }
        }

        #endregion
    }
}
