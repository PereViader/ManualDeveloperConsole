namespace UnityDeveloperConsole.Core
{
    public static class UnityDeveloperConsoleExtensions
    {
        public static void ToggleVisibility(this IUnityDeveloperConsole unityDeveloperConsole)
        {
            if (unityDeveloperConsole.IsVisible)
            {
                unityDeveloperConsole.Hide();
            }
            else
            {
                unityDeveloperConsole.Show();
            }
        }
    }
}
