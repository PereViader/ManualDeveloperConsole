using UnityEditor;
using UnityEngine;

namespace ManualCheats.Core.EditorWidgets.Button
{
    public class ButtonCheatEditorWidget : ICheatEditorWidget
    {
        private ButtonCheat cheat;

        public ButtonCheatEditorWidget(ButtonCheat cheat)
        {
            this.cheat = cheat;
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

            EditorGUILayout.LabelField(cheat.Name);

            if (GUILayout.Button("Run"))
            {
                cheat.Action?.Invoke();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
