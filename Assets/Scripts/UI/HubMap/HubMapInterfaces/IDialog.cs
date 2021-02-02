namespace BeastHunter
{ 
    public interface IDialog
    {
        public int Id { get; }
        public string DialogText { get; }
        public string PositiveAnswer { get; }
        public string NegativeAnswer { get; }
        public bool IsQuest { get; }
    }
}
