using ManualCheats.Core;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ManualCheats.EditorPlugin.Widgets.DropdownButton
{
    public class DropdownButtonCheatEditorWidget<T> : ICheatEditorWidget
    {
        private readonly DropdownButtonCheat<T> cheat;
        private int currentSelected;
        private string[] optionNames;

        public DropdownButtonCheatEditorWidget(DropdownButtonCheat<T> cheat)
        {
            this.cheat = cheat;
        }

        public void Activate()
        {
            optionNames = cheat.OptionNames.ToArray();
        }

        public void Deactivate()
        {
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(cheat.Name);
            currentSelected = EditorGUILayout.Popup(currentSelected, optionNames);

            if (GUILayout.Button("Run"))
            {
                var value = cheat.OptionValues[currentSelected];
                cheat.OnActivate(value);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
