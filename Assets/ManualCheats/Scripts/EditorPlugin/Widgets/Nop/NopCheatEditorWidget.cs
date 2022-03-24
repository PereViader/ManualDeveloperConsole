using ManualCheats.Core;
using UnityEditor;

namespace ManualCheats.EditorPlugin.Widgets.Nop
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
