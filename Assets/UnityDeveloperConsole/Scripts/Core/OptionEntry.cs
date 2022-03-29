namespace UnityDeveloperConsole.Core
{
    public class OptionEntry
    {
        public IOption Option { get; }
        public string Category { get; }

        public OptionEntry(IOption option, string category)
        {
            Option = option;
            Category = category;
        }
    }
}
