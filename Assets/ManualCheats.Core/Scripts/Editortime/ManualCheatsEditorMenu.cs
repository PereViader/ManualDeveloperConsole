using UnityEditor;

namespace ManualCheats.Core.EditorWidgets
{
    public static class ManualCheatsEditorMenu
    {
        [MenuItem("Window/Manual Cheats/Editor")]
        public static void OpenManualCheatsEditor()
        {
            var window = EditorWindow.GetWindow<ManualCheatsEditorWindow>("Manual Cheats");
            window.Show();
        }
    }
}
