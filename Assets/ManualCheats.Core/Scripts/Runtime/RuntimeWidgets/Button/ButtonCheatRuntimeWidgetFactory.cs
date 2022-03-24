using UnityEngine;

namespace ManualCheats.Core
{
    public class ButtonCheatRuntimeWidgetFactory
    {
        private readonly ButtonCheatRuntimeWidget prefab;

        public ButtonCheatRuntimeWidgetFactory(ButtonCheatRuntimeWidget prefab)
        {
            this.prefab = prefab;
        }

        public ButtonCheatRuntimeWidget Create(ButtonCheat buttonCheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            widgetInstance.Inject(buttonCheat);
            widgetInstance.Initialize();

            return widgetInstance;
        }
    }
}
