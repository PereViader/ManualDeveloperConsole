using UnityEngine;

namespace ManualCheats.Core
{
    public class ToggleCheatWidgetFactory
    {
        private readonly ToggleCheatWidget prefab;

        public ToggleCheatWidgetFactory(ToggleCheatWidget prefab)
        {
            this.prefab = prefab;
        }

        public ToggleCheatWidget Create(ToggleCheat toggleCheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            widgetInstance.Inject(toggleCheat);
            widgetInstance.Init();

            return widgetInstance;
        }
    }
}
