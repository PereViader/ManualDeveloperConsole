using UnityEngine;

namespace UnityDeveloperConsole.EditorPlugin
{
    public abstract class EditorConfiguration : ScriptableObject
    {
        public abstract EditorWidgetConfiguration Create();
    }
}
