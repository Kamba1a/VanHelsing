namespace BeastHunter
{ 
    public interface IHubMapUIDialog
    {
        public int Number { get; }
        public string Text { get; }
        public bool IsQuest { get; }
        public IHubMapUIDialogAnswer PositiveAnswer { get; }
        public IHubMapUIDialogAnswer NegativeAnswer { get; }
    }
}
