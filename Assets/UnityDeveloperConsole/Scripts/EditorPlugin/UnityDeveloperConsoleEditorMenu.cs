using UnityEditor;

namespace UnityDeveloperConsole.EditorPlugin
{
    public static class UnityDeveloperConsoleEditorMenu
    {
        [MenuItem("Window/Unity Developer Console/Open Window")]
        public static void OpenUnityDeveloperConsoleWindow()
        {
            var window = EditorWindow.GetWindow<UnityDeveloperConsoleEditorWindow>("Developer Console");
            window.Show();
        }
    }
}
