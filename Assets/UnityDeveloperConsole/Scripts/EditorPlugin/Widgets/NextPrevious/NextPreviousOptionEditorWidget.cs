using System;
using System.Collections.Generic;
using UnityDeveloperConsole.Core;
using UnityDeveloperConsole.EditorPlugin.Utils;
using UnityEditor;
using UnityEngine;

namespace UnityDeveloperConsole.EditorPlugin.Widgets.NextPrevious
{
    public class NextPreviousOptionEditorWidget<T> : IOptionEditorWidget
    {
        private readonly NextPreviousOption<T> option;
        private readonly string textControl;

        private T value;
        private string text;
        private bool textActive;

        public NextPreviousOptionEditorWidget(NextPreviousOption<T> option)
        {
            this.option = option;
            this.textControl = Guid.NewGuid().ToString();
        }

        public void Activate()
        {
            UpdateCurrentValue();
        }

        private void UpdateAndSetValue(T value)
        {
            option.SetValue(value);
            UpdateCurrentValue();
        }

        private void UpdateCurrentValue()
        {
            value = option.GetValue();
            text = option.ConvertValueToString(value);
        }

        public void Deactivate()
        {
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(option.Name);

            if (GUILayout.Button("<", GUILayout.Width(30)))
            {
                UpdateAndSetValue(option.GetPreviousValue(value));
            }

            if (EditorGUIUtils.DetectControlIDChange(TextField, textControl, ref textActive))
            {
                try
                {
                    var newValue = option.ConvertStringToValue(text);
                    if (!EqualityComparer<T>.Default.Equals(value, newValue))
                    {
                        UpdateAndSetValue(newValue);
                    }
                }
                catch
                {
                    Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, "Invalid Value For Cheat {0}", option.Name);
                    UpdateCurrentValue();
                }
            }

            if (GUILayout.Button(">", GUILayout.Width(30)))
            {
                UpdateAndSetValue(option.GetNextValue(value));
            }
            EditorGUILayout.EndHorizontal();
        }

        private void TextField()
        {
            text = EditorGUILayout.TextField(text);
        }
    }
}
