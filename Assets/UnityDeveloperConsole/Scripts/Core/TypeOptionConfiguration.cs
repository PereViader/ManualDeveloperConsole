using System;

namespace UnityDeveloperConsole.Core
{
    public delegate IOptionRuntimeWidget CreateCheatWidgetDelegate(IOption cheat);
    public delegate void DisposeCheatWidgetDelegate(IOptionRuntimeWidget cheatWidget);

    public class TypeOptionConfiguration
    {
        public Predicate<Type> AcceptsTypePredicate { get; }
        public CreateCheatWidgetDelegate CreateOptionWidgetDelegate { get; }
        public DisposeCheatWidgetDelegate DisposeOptionWidgetDelegate { get; }

        public TypeOptionConfiguration(
            Predicate<Type> acceptsTypePredicate,
            CreateCheatWidgetDelegate createCheatWidgetDelegate,
            DisposeCheatWidgetDelegate disposeCheatWidgetDelegate)
        {
            AcceptsTypePredicate = acceptsTypePredicate;
            CreateOptionWidgetDelegate = createCheatWidgetDelegate;
            DisposeOptionWidgetDelegate = disposeCheatWidgetDelegate;
        }
    }
}
