
using System;

namespace UnityDeveloperConsole.Core
{
    public class NextPreviousOption<T> : INextPreviousOption
    {
        public string Name { get; }

        public Action<T> SetValue { get; }
        public Func<T> GetValue { get; }

        public Func<T, T> GetNextValue { get; }
        public Func<T, T> GetPreviousValue { get; }
        public Func<T, string> ConvertValueToString { get; }
        public Func<string, T> ConvertStringToValue { get; }

        public NextPreviousOption(
            string name,
            Action<T> setValue,
            Func<T> getValue,
            Func<T, T> getNextValue,
            Func<T, T> getPreviousValue,
            Func<string, T> convertStringToValue,
            Func<T, string> convertValueToString)
        {
            Name = name;
            SetValue = setValue;
            GetValue = getValue;
            GetNextValue = getNextValue;
            GetPreviousValue = getPreviousValue;
            ConvertStringToValue = convertStringToValue;
            ConvertValueToString = convertValueToString;
        }

        public TReturn Accept<TArg, TReturn>(INextPreviousOptionVisitor<TArg, TReturn> visitor, TArg arg)
        {
            return visitor.Visit(this, arg);
        }
    }
}
