using System;

namespace ManualCheats.Core
{
    public class ToggleCheat : ICheat
    {
        public string Name { get; }

        public Func<bool> GetValue { get; }
        public Action<bool> SetValue { get; }

        public ToggleCheat(string name, Func<bool> getValue, Action<bool> setValue)
        {
            Name = name;
            GetValue = getValue;
            SetValue = setValue;
        }
    }
}
