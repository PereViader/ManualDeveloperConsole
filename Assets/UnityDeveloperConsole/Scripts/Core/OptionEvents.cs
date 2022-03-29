using System;

namespace UnityDeveloperConsole.Core
{
    public static class OptionEvents
    {
        public static event Action<string, IOption> OnOptionAdded;
        public static event Action<IOption> OnOptionRemoved;

        public static void PublishOptionAdded(string category, IOption option)
        {
            OnOptionAdded?.Invoke(category, option);
        }

        public static void PublishOptionRemoved(IOption option)
        {
            OnOptionRemoved?.Invoke(option);
        }
    }
}
