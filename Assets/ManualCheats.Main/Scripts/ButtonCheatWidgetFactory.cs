using UnityEngine;

namespace ManualCheats.Core
{
    public class ButtonCheatWidgetFactory
    {
        private readonly ButtonCheatWidget prefab;

        public ButtonCheatWidgetFactory(ButtonCheatWidget prefab)
        {
            this.prefab = prefab;
        }

        public ButtonCheatWidget Create(ButtonCheat buttonCheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            widgetInstance.Inject(buttonCheat);
            widgetInstance.Init();

            return widgetInstance;
        }
    }
}
