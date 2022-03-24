using UnityEngine;

namespace ManualCheats.Core
{
    [CreateAssetMenu(fileName = nameof(ManualCheatsConfiguration), menuName = "Manual Cheats/Default Runtime Configuration")]
    public class ManualCheatsConfiguration : ScriptableObject
    {
        public ManualCheatsService manualCheatsServicePrefab;
        public ButtonCheatRuntimeWidget buttonCheatWidget;
        public ToggleCheatRuntimeWidget toggleCheatWidget;
        public NextPreviousCheatRuntimeWidgetReferences nextPreviousCheatWidgetReferences;
        public DropwdownButtonCheatRuntimeWidgetReferences dropwdownButtonCheatWidgetReferences;
    }
}
