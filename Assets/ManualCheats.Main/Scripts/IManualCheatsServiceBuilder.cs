using System;

namespace ManualCheats.Core
{
    public interface IManualCheatsServiceBuilder
    {
        IManualCheatsServiceBuilder AddCheat<TCheat, TCheatWidget>(Func<TCheat, TCheatWidget> createWidget, Action<TCheatWidget> disposeWidget)
            where TCheat : ICheat
            where TCheatWidget : ICheatWidget<TCheat>;

        IManualCheatsService Build(ManualCheatsConfiguration manualCheatsConfiguration);
    }
}
