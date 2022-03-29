using System;

namespace UnityDeveloperConsole.Core
{
    public class ButtonOption : IOption
    {
        public Action Action { get; }

        public string Name { get; }

        public ButtonOption(string name, Action action)
        {
            Action = action;
            Name = name;
        }
    }
}
