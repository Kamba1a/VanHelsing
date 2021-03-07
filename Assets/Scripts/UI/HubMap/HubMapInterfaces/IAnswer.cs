namespace BeastHunter
{
    public interface IDialogAnswer
    {
        public string Text { get; }
        public bool IsDialogEnd { get; }
        public int NextDialogNumber { get; }
    }
}
