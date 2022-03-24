using UnityEngine;

namespace ManualCheats.Core.EditorWidgets
{
    public abstract class EditorConfiguration : ScriptableObject
    {
        public abstract EditorWidgetConfiguration Create();
    }
}
