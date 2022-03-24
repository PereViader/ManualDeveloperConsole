using UnityEngine;

namespace ManualCheats.EditorPlugin
{
    public abstract class EditorConfiguration : ScriptableObject
    {
        public abstract EditorWidgetConfiguration Create();
    }
}
