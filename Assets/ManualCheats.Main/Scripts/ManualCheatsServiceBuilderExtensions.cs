﻿namespace ManualCheats.Core
{
    public static class ManualCheatsServiceBuilderExtensions
    {
        public static IManualCheatsServiceBuilder AddDefaultCheats(this IManualCheatsServiceBuilder builder, ManualCheatsConfiguration defaultManualCheatsConfiguration)
        {
            var disposer = new DestroyGameObjectCheatWidgetDisposer();

            var buttonFactory = new ButtonCheatWidgetFactory(defaultManualCheatsConfiguration.buttonCheatWidget);
            builder.AddCheat(new TypeCheatConfiguration(
                typeof(ButtonCheat).IsAssignableFrom,
                x => buttonFactory.Create((ButtonCheat)x),
                disposer.Dispose
                ));

            var nextPreviousFactory = new InstantiateAndCallDelegate<INextPreviousCheat, NextPreviousCheatWidgetReferences, ICheatWidget>(
                defaultManualCheatsConfiguration.nextPreviousCheatWidgetReferences,
                new NextPreviousCheatWidgetFactory().Create
                );
            builder.AddCheat(new TypeCheatConfiguration(
                typeof(INextPreviousCheat).IsAssignableFrom,
                x => nextPreviousFactory.Create((INextPreviousCheat)x),
                disposer.Dispose
                ));

            var toggleFactory = new ToggleCheatWidgetFactory(defaultManualCheatsConfiguration.toggleCheatWidget);
            builder.AddCheat(new TypeCheatConfiguration(
                typeof(ToggleCheat).IsAssignableFrom,
                x => toggleFactory.Create((ToggleCheat)x),
                disposer.Dispose
                ));

            var dropdownFactory = new InstantiateAndCallDelegate<IDropdownButtonCheat, DropwdownButtonCheatWidgetReferences, ICheatWidget>(
                defaultManualCheatsConfiguration.dropwdownButtonCheatWidgetReferences,
                new DropdownButtonCheatWidgetFactory().Create
                );
            builder.AddCheat(new TypeCheatConfiguration(
                typeof(IDropdownButtonCheat).IsAssignableFrom,
                x => dropdownFactory.Create((IDropdownButtonCheat)x),
                disposer.Dispose
                ));

            return builder;
        }
    }
}
