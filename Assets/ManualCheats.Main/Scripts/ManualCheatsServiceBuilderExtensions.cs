namespace ManualCheats.Core
{
    public static class ManualCheatsServiceBuilderExtensions
    {
        public static IManualCheatsServiceBuilder AddDefaultCheats(this IManualCheatsServiceBuilder builder, ManualCheatsConfiguration defaultManualCheatsConfiguration)
        {
            var disposer = new DestroyGameObjectCheatWidgetDisposer();

            var buttonFactory = new ButtonCheatWidgetFactory(defaultManualCheatsConfiguration.buttonCheatWidget);
            builder.AddCheat(new TypeCheatConfiguration(
                t => t == typeof(ButtonCheat),
                x => buttonFactory.Create((ButtonCheat)x),
                disposer.Dispose
                ));

            var nextPreviousFactory = new InstantiateAndCallDelegate<INextPreviousCheat, NextPreviousCheatWidgetReferences, ICheatWidget>(
                defaultManualCheatsConfiguration.nextPreviousCheatWidgetReferences,
                new NextPreviousCheatWidgetFactory().Create
                );
            builder.AddCheat(new TypeCheatConfiguration(
                t => typeof(INextPreviousCheat).IsAssignableFrom(t),
                x => nextPreviousFactory.Create((INextPreviousCheat)x),
                disposer.Dispose
                ));

            var toggleFactory = new ToggleCheatWidgetFactory(defaultManualCheatsConfiguration.toggleCheatWidget);
            builder.AddCheat(new TypeCheatConfiguration(
                t => t == typeof(ToggleCheat),
                x => toggleFactory.Create((ToggleCheat)x),
                disposer.Dispose
                ));

            var dropdownFactory = new InstantiateAndCallDelegate<IDropdownButtonCheat, DropwdownButtonCheatWidgetReferences, ICheatWidget>(
                defaultManualCheatsConfiguration.dropwdownButtonCheatWidgetReferences,
                new DropdownButtonCheatWidgetFactory().Create
                );
            builder.AddCheat(new TypeCheatConfiguration(
                t => typeof(IDropdownButtonCheat).IsAssignableFrom(t),
                x => dropdownFactory.Create((IDropdownButtonCheat)x),
                disposer.Dispose
                ));

            return builder;
        }
    }
}
