using System.Collections.Generic;
using System.Linq;

namespace UnityDeveloperConsole.Core
{
    public class OptionEntryRepository
    {
        private readonly Dictionary<IOption, OptionEntry> cheatEntries = new Dictionary<IOption, OptionEntry>();

        public List<OptionEntry> GetAll()
        {
            return cheatEntries.Values.ToList();
        }

        public bool TryGet(IOption cheat, out OptionEntry cheatEntry)
        {
            return cheatEntries.TryGetValue(cheat, out cheatEntry);
        }

        public void Add(IOption cheat, OptionEntry cheatEntry)
        {
            cheatEntries.Add(cheat, cheatEntry);
        }

        public bool Remove(IOption cheat)
        {
            return cheatEntries.Remove(cheat);
        }

        public bool Contains(IOption cheat)
        {
            return cheatEntries.ContainsKey(cheat);
        }
    }
}
