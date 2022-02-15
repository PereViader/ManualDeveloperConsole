using System;

namespace ManualCheats.Core
{
    public delegate ICheatWidget CreateCheatWidgetDelegate(ICheat cheat);
    public delegate void DisposeCheatWidgetDelegate(ICheatWidget cheatWidget);

    public class TypeCheatConfiguration
    {
        public Predicate<Type> AcceptsTypePredicate { get; }
        public CreateCheatWidgetDelegate CreateCheatWidgetDelegate { get; }
        public DisposeCheatWidgetDelegate DisposeCheatWidgetDelegate { get; }

        public TypeCheatConfiguration(
            Predicate<Type> acceptsTypePredicate,
            CreateCheatWidgetDelegate createCheatWidgetDelegate,
            DisposeCheatWidgetDelegate disposeCheatWidgetDelegate)
        {
            AcceptsTypePredicate = acceptsTypePredicate;
            CreateCheatWidgetDelegate = createCheatWidgetDelegate;
            DisposeCheatWidgetDelegate = disposeCheatWidgetDelegate;
        }
    }
}
