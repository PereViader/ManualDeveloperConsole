using System;

namespace ManualCheats.Core
{
    public static class CheatEvents
    {
        public static event Action<string, ICheat> OnCheatAdded;
        public static event Action<ICheat> OnCheatRemoved;

        public static void PublishCheatAdded(string category, ICheat cheat)
        {
            OnCheatAdded?.Invoke(category, cheat);
        }

        public static void PublishCheatRemoved(ICheat cheat)
        {
            OnCheatRemoved?.Invoke(cheat);
        }
    }
}
