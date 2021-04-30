namespace BeastHunterHubUI
{
    public class CharacterCheckNameService
    {
        #region Fields

        private HubUIContext _context;

        #endregion


        #region ClassLifeCycle

        public CharacterCheckNameService(HubUIContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        public bool IsNameFree(string name)
        {
            for (int i = 0; i < _context.Player.HiredCharacters.Count; i++)
            {
                if (_context.Player.HiredCharacters[i].Name == name)
                {
                    return false;
                }
            }

            for (int i = 0; i < _context.CharactersForHire.Count; i++)
            {
                if (_context.CharactersForHire[i].Name == name)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
