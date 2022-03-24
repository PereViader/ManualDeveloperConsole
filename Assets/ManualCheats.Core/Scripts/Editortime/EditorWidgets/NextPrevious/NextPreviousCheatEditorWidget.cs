using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ManualCheats.Core.EditorWidgets.NextPrevious
{
    public class NextPreviousCheatEditorWidget<T> : ICheatEditorWidget
    {
        private NextPreviousCheat<T> cheat;
        private T value;
        private string text;
        private bool textActive;
        private string textControl;

        public NextPreviousCheatEditorWidget(NextPreviousCheat<T> cheat)
        {
            this.cheat = cheat;
            this.textControl = Guid.NewGuid().ToString();
        }

        public void Activate()
        {
            UpdateCurrentValue();
        }

        private void UpdateAndSetValue(T value)
        {
            cheat.SetValue(value);
            UpdateCurrentValue();
        }

        private void UpdateCurrentValue()
        {
            value = cheat.GetValue();
            text = cheat.ConvertValueToString(value);
        }

        public void Deactivate()
        {
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(cheat.Name);

            if (GUILayout.Button("<", GUILayout.Width(30)))
            {
                UpdateAndSetValue(cheat.GetPreviousValue(value));
            }

            if (EditorGUIUtils.DetectControlIDChange(TextField, textControl, ref textActive))
            {
                try
                {
                    var newValue = cheat.ConvertStringToValue(text);
                    if (!EqualityComparer<T>.Default.Equals(value, newValue))
                    {
                        UpdateAndSetValue(newValue);
                    }
                }
                catch
                {
                    Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, "Invalid Value For Cheat {0}", cheat.Name);
                    UpdateCurrentValue();
                }
            }

            if (GUILayout.Button(">", GUILayout.Width(30)))
            {
                UpdateAndSetValue(cheat.GetNextValue(value));
            }
            EditorGUILayout.EndHorizontal();
        }

        private void TextField()
        {
            text = EditorGUILayout.TextField(text);
        }
    }
}
