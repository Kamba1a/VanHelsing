namespace BeastHunter
{ 
    public interface IHubMapDialog
    {
        public int Number { get; }
        public string Text { get; }
        public bool IsQuest { get; }
        public IHubMapDialogAnswer PositiveAnswer { get; }
        public IHubMapDialogAnswer NegativeAnswer { get; }
    }
}
