namespace UnityDeveloperConsole.EditorPlugin.Widgets
{
    public interface IOptionEditorWidget
    {
        void Activate();
        void Deactivate();

        void OnGUI();
    }
}
