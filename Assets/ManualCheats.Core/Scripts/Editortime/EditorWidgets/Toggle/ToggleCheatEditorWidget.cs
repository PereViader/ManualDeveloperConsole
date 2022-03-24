using UnityEditor;

namespace ManualCheats.Core.EditorWidgets.Toggle
{
    public class ToggleCheatEditorWidget : ICheatEditorWidget
    {
        private ToggleCheat toggleCheat;

        public ToggleCheatEditorWidget(ToggleCheat toggleCheat)
        {
            this.toggleCheat = toggleCheat;
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
            bool value = toggleCheat.GetValue();
            EditorGUILayout.LabelField(toggleCheat.Name);
            bool newValue = EditorGUILayout.Toggle(value);
            if (newValue != value)
            {
                toggleCheat.SetValue(newValue);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
