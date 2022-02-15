using System;

namespace ManualCheats.Core
{
    public class ButtonCheat : ICheat
    {
        public Action Action { get; }

        public string Name { get; }

        public ButtonCheat(string name, Action action)
        {
            Action = action;
            Name = name;
        }
    }
}
