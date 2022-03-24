namespace ManualCheats.EditorPlugin.Widgets
{
    public interface ICheatEditorWidget
    {
        void Activate();
        void Deactivate();

        void OnGUI();
    }
}
