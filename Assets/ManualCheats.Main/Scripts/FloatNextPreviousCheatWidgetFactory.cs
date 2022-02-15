using UnityEngine;

namespace ManualCheats.Core
{
    public class FloatNextPreviousCheatWidgetFactory
    {
        private readonly CommonNextPreviousCheatWidget prefab;

        public FloatNextPreviousCheatWidgetFactory(CommonNextPreviousCheatWidget prefab)
        {
            this.prefab = prefab;
        }

        public TypeAdapterNextPreviousCheatWidget<FloatNextPreviousCheat, float> Create(FloatNextPreviousCheat cheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            var typeWidget = new TypeAdapterNextPreviousCheatWidget<FloatNextPreviousCheat, float>(
                cheat,
                widgetInstance
                );

            typeWidget.Init();

            return typeWidget;
        }
    }
}
