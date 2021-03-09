namespace BeastHunter
{
    public interface IHubMapUIDialogAnswer
    {
        public string Text { get; }
        public bool IsDialogEnd { get; }
        public int NextDialogNumber { get; }
    }
}
