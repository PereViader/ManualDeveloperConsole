using UnityDeveloperConsole.Core;
using UnityEditor;

namespace UnityDeveloperConsole.EditorPlugin.Widgets.Nop
{
    public class NopOptionEditorWidget : IOptionEditorWidget
    {
        private readonly IOption option;

        public NopOptionEditorWidget(IOption option)
        {
            this.option = option;
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public void OnGUI()
        {
            EditorGUILayout.LabelField("Option without Editor Widget", option.Name);
        }
    }
}
