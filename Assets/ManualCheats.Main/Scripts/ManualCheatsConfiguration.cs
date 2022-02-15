using UnityEngine;

namespace ManualCheats.Core
{
    [CreateAssetMenu(fileName = nameof(ManualCheatsConfiguration), menuName = "ManualCheats/DefaultConfiguration")]
    public class ManualCheatsConfiguration : ScriptableObject
    {
        public ManualCheatsService manualCheatsServicePrefab;
        public ButtonCheatWidget buttonCheatWidget;
        public ToggleCheatWidget toggleCheatWidget;
        public NextPreviousCheatWidgetReferences nextPreviousCheatWidgetReferences;
        public DropwdownButtonCheatWidgetReferences dropwdownButtonCheatWidgetReferences;
    }
}
