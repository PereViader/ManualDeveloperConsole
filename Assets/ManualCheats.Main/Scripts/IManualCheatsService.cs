namespace ManualCheats.Core
{
    public interface IManualCheatsService
    {
        bool IsVisible { get; }

        void Show();
        void Hide();

        void AddCheat(string category, ICheat cheat);
        void RemoveCheat(ICheat cheat);
    }
}
