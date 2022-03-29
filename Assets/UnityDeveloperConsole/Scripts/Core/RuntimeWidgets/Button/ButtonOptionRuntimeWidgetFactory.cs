using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class ButtonOptionRuntimeWidgetFactory
    {
        private readonly ButtonOptionRuntimeWidget prefab;

        public ButtonOptionRuntimeWidgetFactory(ButtonOptionRuntimeWidget prefab)
        {
            this.prefab = prefab;
        }

        public ButtonOptionRuntimeWidget Create(ButtonOption buttonCheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            widgetInstance.Inject(buttonCheat);
            widgetInstance.Initialize();

            return widgetInstance;
        }
    }
}
