using UnityEditor;

namespace ManualCheats.Core.EditorWidgets.Nop
{
    public class NopCheatEditorWidget : ICheatEditorWidget
    {
        private ICheat cheat;

        public NopCheatEditorWidget(ICheat cheat)
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
            EditorGUILayout.LabelField("Cheat without Editor Widget", cheat.Name);
        }
    }
}
