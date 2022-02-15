namespace ManualCheats.Core
{
    public static class ManualCheatsServiceBuilderExtensions
    {
        public static IManualCheatsServiceBuilder AddDefaultCheats(this IManualCheatsServiceBuilder builder, ManualCheatsConfiguration defaultManualCheatsConfiguration)
        {
            var disposer = new DestroyGameObjectCheatWidgetDisposer();

            builder.AddCheat<ButtonCheat, ButtonCheatWidget>(
                new ButtonCheatWidgetFactory(defaultManualCheatsConfiguration.buttonCheatWidget).Create,
                disposer.Dispose
                );

            builder.AddCheat<IntNextPreviousCheat, TypeAdapterNextPreviousCheatWidget<IntNextPreviousCheat, int>>(
                new IntNextPreviousCheatWidgetFactory(defaultManualCheatsConfiguration.nextPreviousCheatWidget).Create,
                disposer.Dispose
                );

            builder.AddCheat<FloatNextPreviousCheat, TypeAdapterNextPreviousCheatWidget<FloatNextPreviousCheat, float>>(
                new FloatNextPreviousCheatWidgetFactory(defaultManualCheatsConfiguration.nextPreviousCheatWidget).Create,
                disposer.Dispose
                );

            builder.AddCheat<ToggleCheat, ToggleCheatWidget>(
                new ToggleCheatWidgetFactory(defaultManualCheatsConfiguration.toggleCheatWidget).Create,
                disposer.Dispose
                );

            return builder;
        }
    }
}
