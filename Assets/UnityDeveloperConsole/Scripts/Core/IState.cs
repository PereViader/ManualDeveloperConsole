namespace UnityDeveloperConsole.Core
{
    public interface IState
    {
        void OnOptionAdded(OptionEntry optionEntry);
        void OnOptionRemoved(OptionEntry optionEntry);
        void OnEnter();
        void OnExit();
        void Activate();
        void Deactivate();
    }
}
