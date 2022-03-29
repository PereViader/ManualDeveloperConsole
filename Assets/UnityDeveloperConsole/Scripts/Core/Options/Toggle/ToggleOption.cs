using System;

namespace UnityDeveloperConsole.Core
{
    public class ToggleOption : IOption
    {
        public string Name { get; }

        public Func<bool> GetValue { get; }
        public Action<bool> SetValue { get; }

        public ToggleOption(string name, Func<bool> getValue, Action<bool> setValue)
        {
            Name = name;
            GetValue = getValue;
            SetValue = setValue;
        }
    }
}
