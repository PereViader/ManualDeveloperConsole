using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public interface IOptionRuntimeWidget
    {
        GameObject GameObject { get; }

        void Activate();
        void Deactivate();
    }
}
