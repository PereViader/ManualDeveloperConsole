using System.Linq;
using UnityDeveloperConsole.Core;
using UnityEditor;
using UnityEngine;

namespace UnityDeveloperConsole.EditorPlugin.Widgets.DropdownButton
{
    public class DropdownButtonOptionEditorWidget<T> : IOptionEditorWidget
    {
        private readonly DropdownButtonOption<T> option;

        private int currentSelected;
        private string[] optionNames;

        public DropdownButtonOptionEditorWidget(DropdownButtonOption<T> option)
        {
            this.option = option;
        }

        public void Activate()
        {
            optionNames = option.OptionNames.ToArray();
        }

        public void Deactivate()
        {
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(option.Name);
            currentSelected = EditorGUILayout.Popup(currentSelected, optionNames);

            if (GUILayout.Button("Run"))
            {
                var value = option.OptionValues[currentSelected];
                option.OnActivate(value);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
