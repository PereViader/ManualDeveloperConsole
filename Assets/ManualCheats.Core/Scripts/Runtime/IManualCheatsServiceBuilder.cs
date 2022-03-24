namespace ManualCheats.Core
{
    public interface IManualCheatsServiceBuilder
    {
        IManualCheatsServiceBuilder AddCheat(TypeCheatConfiguration typeCheatConfiguration);

        IManualCheatsService Build(ManualCheatsConfiguration manualCheatsConfiguration);
    }
}
