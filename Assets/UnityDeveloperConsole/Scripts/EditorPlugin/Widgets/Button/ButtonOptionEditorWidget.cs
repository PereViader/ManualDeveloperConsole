using UnityDeveloperConsole.Core;
using UnityEditor;
using UnityEngine;

namespace UnityDeveloperConsole.EditorPlugin.Widgets.Button
{
    public class ButtonOptionEditorWidget : IOptionEditorWidget
    {
        private readonly ButtonOption option;

        public ButtonOptionEditorWidget(ButtonOption option)
        {
            this.option = option;
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(option.Name);

            if (GUILayout.Button("Run"))
            {
                option.Action?.Invoke();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
