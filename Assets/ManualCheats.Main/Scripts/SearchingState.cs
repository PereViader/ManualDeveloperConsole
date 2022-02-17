using System;

namespace ManualCheats.Core
{
    public class SearchingState : IState
    {
        private readonly DisplayingAllState displayingAllState;
        private readonly CheatEntryRepository cheatEntryRepository;

        private Predicate<ICheat> displayCheatPredicate;
        private bool isEntered;

        public SearchingState(
            DisplayingAllState displayingAllState,
            CheatEntryRepository cheatEntryRepository)
        {
            this.displayingAllState = displayingAllState;
            this.cheatEntryRepository = cheatEntryRepository;
        }

        public void OnDisplayCheatPredicateChanged(Predicate<ICheat> predicate)
        {
            displayCheatPredicate = predicate;
        }

        public void OnEnter()
        {
            foreach (var cheatEntry in cheatEntryRepository.GetAll())
            {
                OnCheatAdded(cheatEntry);
            }
            isEntered = true;
        }

        public void OnExit()
        {
            displayingAllState.OnExit();
            isEntered = false;
        }

        public void Restart()
        {
            OnExit();
            OnEnter();
        }

        public void OnCheatAdded(CheatEntry cheatEntry)
        {
            if (!displayCheatPredicate.Invoke(cheatEntry.Cheat))
            {
                return;
            }

            displayingAllState.OnCheatAdded(cheatEntry);
        }

        public void OnCheatRemoved(CheatEntry cheatEntry)
        {
            if (!displayingAllState.Contains(cheatEntry))
            {
                return;
            }

            displayingAllState.OnCheatRemoved(cheatEntry);
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
