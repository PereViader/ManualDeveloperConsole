using System;
using TMPro;
using UnityEngine;

namespace ManualCheats.Core
{
    public class ManualCheatsService : MonoBehaviour, IManualCheatsService
    {
        public GameObject canvasGameObject;
        public ManualCheatsWidgetContainerController containerController;
        public TMP_InputField searchInputField;

        private CheatEntryRepository cheatEntryRepository;
        private DisplayingAllState displayingAllState;
        private SearchingState searchingState;
        private IState currentState;

        public bool IsVisible { get; set; }

        public void Inject(
            CheatEntryRepository cheatEntryRepository,
            DisplayingAllState displayingAllState,
            SearchingState searchingState)
        {
            this.cheatEntryRepository = cheatEntryRepository;
            this.displayingAllState = displayingAllState;
            this.searchingState = searchingState;

            currentState = displayingAllState;
        }

        public void OnEnable()
        {
            searchInputField.onEndEdit.AddListener(SearchInputField_OnEndEdit);
        }

        public void OnDisable()
        {
            searchInputField.onEndEdit.RemoveListener(SearchInputField_OnEndEdit);
        }

        private void SearchInputField_OnEndEdit(string arg0)
        {
            if (string.IsNullOrEmpty(arg0))
            {
                ChangeCurrentState(displayingAllState);
            }
            else
            {
                searchingState.OnDisplayCheatPredicateChanged(x => x.Name.Contains(arg0));
                ChangeCurrentState(searchingState, searchingState.Restart);
            }
        }

        private void ChangeCurrentState(IState nextState, Action stateIsSame = null)
        {
            if (currentState == nextState)
            {
                stateIsSame?.Invoke();
                return;
            }

            currentState.Deactivate();
            currentState.OnExit();

            currentState = nextState;

            currentState.OnEnter();
            currentState.Activate();
        }

        public void AddCheat(string category, ICheat cheat)
        {
            if (cheatEntryRepository.Contains(cheat))
            {
                throw new InvalidOperationException("Cheat is already registered");
            }

            var cheatEntry = new CheatEntry(cheat, category);
            cheatEntryRepository.Add(cheat, cheatEntry);

            currentState.OnCheatAdded(cheatEntry);
        }

        public void RemoveCheat(ICheat cheat)
        {
            if (!cheatEntryRepository.TryGet(cheat, out var cheatEntry))
            {
                throw new InvalidOperationException("Cheat was not registered");
            }

            cheatEntryRepository.Remove(cheat);
            currentState.OnCheatRemoved(cheatEntry);
        }

        public void Show()
        {
            canvasGameObject.SetActive(true);

            currentState.Activate();

            IsVisible = true;
        }

        public void Hide()
        {
            currentState.Deactivate();

            canvasGameObject.SetActive(false);

            IsVisible = false;
        }
    }
}
