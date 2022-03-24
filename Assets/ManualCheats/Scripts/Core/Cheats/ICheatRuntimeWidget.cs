using UnityEngine;

namespace ManualCheats.Core
{
    public interface ICheatRuntimeWidget
    {
        GameObject GameObject { get; }

        void Activate();
        void Deactivate();
    }
}
