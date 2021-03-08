namespace BeastHunter
{
    public interface IHubMapDialogAnswer
    {
        public string Text { get; }
        public bool IsDialogEnd { get; }
        public int NextDialogNumber { get; }
    }
}
