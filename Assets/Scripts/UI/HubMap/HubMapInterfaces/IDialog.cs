namespace BeastHunter
{ 
    public interface IDialog
    {
        public int Number { get; }
        public string Text { get; }
        public bool IsQuest { get; }
        public IDialogAnswer PositiveAnswer { get; }
        public IDialogAnswer NegativeAnswer { get; }
    }
}
