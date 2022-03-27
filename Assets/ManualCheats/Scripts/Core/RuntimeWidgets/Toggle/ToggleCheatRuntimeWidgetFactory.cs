using UnityEngine;

namespace ManualCheats.Core
{
    public class ToggleCheatRuntimeWidgetFactory
    {
        private readonly ToggleCheatRuntimeWidget prefab;

        public ToggleCheatRuntimeWidgetFactory(ToggleCheatRuntimeWidget prefab)
        {
            this.prefab = prefab;
        }

        public ToggleCheatRuntimeWidget Create(ToggleCheat toggleCheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            widgetInstance.Inject(toggleCheat);
            widgetInstance.Initialize();

            return widgetInstance;
        }
    }
}
