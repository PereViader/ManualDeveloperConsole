using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class ToggleCheatRuntimeWidgetFactory
    {
        private readonly ToggleOptionRuntimeWidget prefab;

        public ToggleCheatRuntimeWidgetFactory(ToggleOptionRuntimeWidget prefab)
        {
            this.prefab = prefab;
        }

        public ToggleOptionRuntimeWidget Create(ToggleOption toggleCheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            widgetInstance.Inject(toggleCheat);
            widgetInstance.Initialize();

            return widgetInstance;
        }
    }
}
