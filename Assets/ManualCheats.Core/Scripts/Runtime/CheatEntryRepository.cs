using System.Collections.Generic;
using System.Linq;

namespace ManualCheats.Core
{
    public class CheatEntryRepository
    {
        private readonly Dictionary<ICheat, CheatEntry> cheatEntries = new Dictionary<ICheat, CheatEntry>();

        public List<CheatEntry> GetAll()
        {
            return cheatEntries.Values.ToList();
        }

        public bool TryGet(ICheat cheat, out CheatEntry cheatEntry)
        {
            return cheatEntries.TryGetValue(cheat, out cheatEntry);
        }

        public void Add(ICheat cheat, CheatEntry cheatEntry)
        {
            cheatEntries.Add(cheat, cheatEntry);
        }

        public bool Remove(ICheat cheat)
        {
            return cheatEntries.Remove(cheat);
        }

        public bool Contains(ICheat cheat)
        {
            return cheatEntries.ContainsKey(cheat);
        }
    }
}
