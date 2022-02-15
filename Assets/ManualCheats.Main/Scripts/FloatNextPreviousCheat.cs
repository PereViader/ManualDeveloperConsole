using System;

namespace ManualCheats.Core
{
    public class FloatNextPreviousCheat : BaseNextPreviousCheat<float>, ICheat
    {
        public FloatNextPreviousCheat(
            string name,
            Action<float> setValue,
            Func<float> getValue,
            Func<float, float> getNextValue,
            Func<float, float> getPreviousValue)
            : base(name, setValue, getValue, getNextValue, getPreviousValue, float.Parse, Convert.ToString)
        {
        }
    }
}
