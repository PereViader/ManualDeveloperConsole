using UnityEngine;

namespace ManualCheats.Core
{
    public class DestroyGameObjectCheatWidgetDisposer
    {
        public void Dispose<TCheatWidget>(TCheatWidget cheatWidget)
            where TCheatWidget : ICheatWidget
        {
            GameObject.Destroy(cheatWidget.GameObject);
        }
    }
}
