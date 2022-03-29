using System;

namespace UnityDeveloperConsole.Core
{
    public class SearchingState : IState
    {
        private readonly DisplayingAllState displayingAllState;
        private readonly OptionEntryRepository optionEntryRepository;

        private Predicate<IOption> displayOptionPredicate;

        public SearchingState(
            DisplayingAllState displayingAllState,
            OptionEntryRepository optionEntryRepository)
        {
            this.displayingAllState = displayingAllState;
            this.optionEntryRepository = optionEntryRepository;
        }

        public void OnDisplayCheatPredicateChanged(Predicate<IOption> displayOptionPredicate)
        {
            this.displayOptionPredicate = displayOptionPredicate;
        }

        public void OnEnter()
        {
            foreach (var optionEntry in optionEntryRepository.GetAll())
            {
                OnOptionAdded(optionEntry);
            }
        }

        public void OnExit()
        {
            displayingAllState.OnExit();
        }

        public void Restart()
        {
            OnExit();
            OnEnter();
        }

        public void OnOptionAdded(OptionEntry optionEntry)
        {
            if (!displayOptionPredicate.Invoke(optionEntry.Option))
            {
                return;
            }

            displayingAllState.OnOptionAdded(optionEntry);
        }

        public void OnOptionRemoved(OptionEntry optionEntry)
        {
            if (!displayingAllState.Contains(optionEntry))
            {
                return;
            }

            displayingAllState.OnOptionRemoved(optionEntry);
        }

        public void Activate()
        {
            displayingAllState.Activate();
        }

        public void Deactivate()
        {
            displayingAllState.Deactivate();
        }
    }
}
