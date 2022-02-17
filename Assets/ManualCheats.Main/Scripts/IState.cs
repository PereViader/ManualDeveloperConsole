namespace ManualCheats.Core
{
    public interface IState
    {
        void OnCheatAdded(CheatEntry cheatEntry);
        void OnCheatRemoved(CheatEntry cheatEntry);
        void OnEnter();
        void OnExit();
        void Activate();
        void Deactivate();
    }
}
