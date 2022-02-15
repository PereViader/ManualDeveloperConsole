using System;
using System.Collections.Generic;

namespace ManualCheats.Core
{
    public class DropdownButtonCheat<T> : IDropdownButtonCheat
    {
        public string Name { get; }

        public IReadOnlyList<T> OptionValues { get; }
        public IReadOnlyList<string> OptionNames { get; }

        public Action<T> OnActivate { get; }

        public DropdownButtonCheat(string name, IReadOnlyList<T> optionValues, IReadOnlyList<string> optionNames, Action<T> onActivate)
        {
            Name = name;
            OptionValues = optionValues;
            OptionNames = optionNames;
            OnActivate = onActivate;
        }

        public TReturn Accept<TArg, TReturn>(IDropdownButtonCheatVisitor<TArg, TReturn> visitor, TArg arg)
        {
            return visitor.Visit(this, arg);
        }
    }
}
