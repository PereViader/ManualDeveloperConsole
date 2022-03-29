using System;

namespace UnityDeveloperConsole.Core
{
    public static class UnityDeveloperConsoleBuilderExtensions
    {
        public static IUnityDeveloperConsoleBuilder AddDefaultOptions(this IUnityDeveloperConsoleBuilder builder, UnityDeveloperConsoleRuntimeConfiguration runtimeConfiguration)
        {
            var buttonFactory = new ButtonOptionRuntimeWidgetFactory(runtimeConfiguration.buttonCheatWidget);
            builder.AddOption(Create<ButtonOption>(
                buttonFactory.Create,
                DestroyGameObjectOptionWidgetDisposer.Dispose
                ));

            var nextPreviousFactory = new InstantiateAndCallDelegate<INextPreviousOption, NextPreviousOptionRuntimeWidgetReferences, IOptionRuntimeWidget>(
                runtimeConfiguration.nextPreviousCheatWidgetReferences,
                new NextPreviousCheatRuntimeWidgetFactory().Create
                );
            builder.AddOption(Create<INextPreviousOption>(
                nextPreviousFactory.Create,
                DestroyGameObjectOptionWidgetDisposer.Dispose
                ));

            var toggleFactory = new ToggleCheatRuntimeWidgetFactory(runtimeConfiguration.toggleCheatWidget);
            builder.AddOption(Create<ToggleOption>(
                toggleFactory.Create,
                DestroyGameObjectOptionWidgetDisposer.Dispose
                ));

            var dropdownFactory = new InstantiateAndCallDelegate<IDropdownButtonOption, DropwdownButtonOptionRuntimeWidgetReferences, IOptionRuntimeWidget>(
                runtimeConfiguration.dropwdownButtonCheatWidgetReferences,
                new DropdownButtonOptionRuntimeWidgetFactory().Create
                );
            builder.AddOption(Create<IDropdownButtonOption>(
                dropdownFactory.Create,
                DestroyGameObjectOptionWidgetDisposer.Dispose
                ));

            return builder;
        }

        public static TypeOptionConfiguration Create<T>(Func<T, IOptionRuntimeWidget> createOptionRuntimeWidget, Action<IOptionRuntimeWidget> disposeOptionRuntimeWidget)
            where T : IOption
        {
            return new TypeOptionConfiguration(
                typeof(T).IsAssignableFrom,
                x => createOptionRuntimeWidget.Invoke((T)x),
                disposeOptionRuntimeWidget.Invoke
                );
        }
    }
}
