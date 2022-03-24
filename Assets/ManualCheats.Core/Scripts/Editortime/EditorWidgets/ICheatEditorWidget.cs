namespace ManualCheats.Core.EditorWidgets
{
    public interface ICheatEditorWidget
    {
        void Activate();
        void Deactivate();

        void OnGUI();
    }
}
