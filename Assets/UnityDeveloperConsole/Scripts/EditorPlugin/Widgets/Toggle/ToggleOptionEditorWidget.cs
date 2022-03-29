using UnityDeveloperConsole.Core;
using UnityEditor;

namespace UnityDeveloperConsole.EditorPlugin.Widgets.Toggle
{
    public class ToggleOptionEditorWidget : IOptionEditorWidget
    {
        private readonly ToggleOption toggleOption;

        public ToggleOptionEditorWidget(ToggleOption toggleOption)
        {
            this.toggleOption = toggleOption;
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
            bool value = toggleOption.GetValue();
            EditorGUILayout.LabelField(toggleOption.Name);
            bool newValue = EditorGUILayout.Toggle(value);
            if (newValue != value)
            {
                toggleOption.SetValue(newValue);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
