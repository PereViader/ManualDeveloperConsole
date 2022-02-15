using UnityEngine;

namespace ManualCheats.Core
{
    public interface ICheatWidget
    {
        GameObject GameObject { get; }

        void Activate();
        void Deactivate();
    }
}
