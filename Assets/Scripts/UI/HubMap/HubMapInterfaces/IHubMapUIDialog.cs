namespace BeastHunter
{ 
    public interface IHubMapUIDialog
    {
        public int Number { get; }
        public string Text { get; }
        public bool IsQuest { get; }
        public IHubMapUIAnswer PositiveAnswer { get; }
        public IHubMapUIAnswer NegativeAnswer { get; }
    }
}
