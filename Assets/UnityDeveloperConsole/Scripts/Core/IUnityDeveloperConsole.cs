namespace UnityDeveloperConsole.Core
{
    public interface IUnityDeveloperConsole
    {
        bool IsVisible { get; }

        void Show();
        void Hide();

        void AddOption(string category, IOption option);
        void RemoveOption(IOption option);
    }
}
