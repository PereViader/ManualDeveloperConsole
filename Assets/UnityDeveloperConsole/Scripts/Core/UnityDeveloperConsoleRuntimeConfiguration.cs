using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    [CreateAssetMenu(fileName = nameof(UnityDeveloperConsoleRuntimeConfiguration), menuName = "Unity Developer Console/Default Runtime Configuration")]
    public class UnityDeveloperConsoleRuntimeConfiguration : ScriptableObject
    {
        public UnityDeveloperConsole unityDeveloperConsole;
        public ButtonOptionRuntimeWidget buttonCheatWidget;
        public ToggleOptionRuntimeWidget toggleCheatWidget;
        public NextPreviousOptionRuntimeWidgetReferences nextPreviousCheatWidgetReferences;
        public DropwdownButtonOptionRuntimeWidgetReferences dropwdownButtonCheatWidgetReferences;
    }
}
