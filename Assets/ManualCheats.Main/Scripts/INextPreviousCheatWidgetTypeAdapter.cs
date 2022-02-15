
using System;

namespace ManualCheats.Core
{
    public interface INextPreviousCheatWidgetTypeAdapter
    {
        event Action<string> OnValueChanged;

        void Update();
        void ChangeValueFromInputField(string arg0);
        void ChangeValueFromPrevious();
        void ChangeValueFromNext();
    }
}
