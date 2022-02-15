using System;

namespace ManualCheats.Core
{
    public class IntNextPreviousCheat : BaseNextPreviousCheat<int>, ICheat
    {
        public IntNextPreviousCheat(
            string name,
            Action<int> setValue,
            Func<int> getValue,
            Func<int, int> getNextValue,
            Func<int, int> getPreviousValue)
            : base(name, setValue, getValue, getNextValue, getPreviousValue, int.Parse, Convert.ToString)
        {
        }
    }
}
