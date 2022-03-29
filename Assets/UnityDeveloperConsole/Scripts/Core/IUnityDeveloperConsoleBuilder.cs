namespace UnityDeveloperConsole.Core
{
    public interface IUnityDeveloperConsoleBuilder
    {
        IUnityDeveloperConsoleBuilder AddOption(TypeOptionConfiguration typeOptionConfiguration);

        IUnityDeveloperConsole Build(UnityDeveloperConsoleRuntimeConfiguration unityDeveloperConsoleRuntimeConfiguration);
    }
}
