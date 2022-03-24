namespace ManualCheats.Core
{
    public class CheatEntry
    {
        public ICheat Cheat { get; }
        public string Category { get; }

        public CheatEntry(ICheat cheat, string category)
        {
            Cheat = cheat;
            Category = category;
        }
    }
}
