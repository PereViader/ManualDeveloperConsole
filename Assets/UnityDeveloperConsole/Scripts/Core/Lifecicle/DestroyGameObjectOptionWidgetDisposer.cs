using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public static class DestroyGameObjectOptionWidgetDisposer
    {
        public static void Dispose(IOptionRuntimeWidget optionWidget)
        {
            GameObject.Destroy(optionWidget.GameObject);
        }
    }
}
