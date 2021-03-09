namespace BeastHunter
{
    public interface IHubMapUIAnswer
    {
        public string Text { get; }
        public bool IsDialogEnd { get; }
        public int NextDialogNumber { get; }
    }
}
