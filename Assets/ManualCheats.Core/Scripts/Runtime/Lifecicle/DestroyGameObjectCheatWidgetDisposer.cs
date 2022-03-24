using UnityEngine;

namespace ManualCheats.Core
{
    public class DestroyGameObjectCheatWidgetDisposer
    {
        public void Dispose<TCheatWidget>(TCheatWidget cheatWidget)
            where TCheatWidget : ICheatRuntimeWidget
        {
            GameObject.Destroy(cheatWidget.GameObject);
        }
    }
}
