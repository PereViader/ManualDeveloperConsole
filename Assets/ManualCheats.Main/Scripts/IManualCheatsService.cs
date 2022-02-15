namespace ManualCheats.Core
{
    public interface IManualCheatsService
    {
        bool IsVisible { get; }

        void Show();
        void Hide();

        void AddCheat<T>(string category, T cheat) where T : ICheat;
        void RemoveCheat<T>(T cheat) where T : ICheat;
    }
}
