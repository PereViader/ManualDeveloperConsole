using System;
using UnityEngine;

namespace ManualCheats.Core.EditorWidgets.NextPrevious
{
    public static class EditorGUIUtils
    {
        public static bool DetectControlIDChange(Action onGUI, string controlName, ref bool wasActive)
        {
            GUI.SetNextControlName(controlName);
            onGUI.Invoke();
            var focused = GUI.GetNameOfFocusedControl();
            var isActive = controlName.Equals(focused);
            if (wasActive && !isActive)
            {
                wasActive = false;
                return true;
            }

            if (!wasActive && isActive)
            {
                wasActive = true;
            }

            return false;
        }
    }
}
