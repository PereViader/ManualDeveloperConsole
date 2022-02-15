using UnityEngine;

namespace ManualCheats.Core
{
    public class IntNextPreviousCheatWidgetFactory
    {
        private readonly CommonNextPreviousCheatWidget prefab;

        public IntNextPreviousCheatWidgetFactory(CommonNextPreviousCheatWidget prefab)
        {
            this.prefab = prefab;
        }

        public TypeAdapterNextPreviousCheatWidget<IntNextPreviousCheat, int> Create(IntNextPreviousCheat cheat)
        {
            var widgetInstance = GameObject.Instantiate(prefab);

            var typeWidget = new TypeAdapterNextPreviousCheatWidget<IntNextPreviousCheat, int>(
                cheat,
                widgetInstance
                );

            typeWidget.Init();

            return typeWidget;
        }
    }
}
