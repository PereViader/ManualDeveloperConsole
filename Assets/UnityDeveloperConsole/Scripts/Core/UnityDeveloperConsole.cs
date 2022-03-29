using System;
using TMPro;
using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class UnityDeveloperConsole : MonoBehaviour, IUnityDeveloperConsole
    {
        public GameObject canvasGameObject;
        public UnityDeveloperConsoleWidgetContainerController containerController;
        public TMP_InputField searchInputField;

        private OptionEntryRepository optionEntryRepository;
        private DisplayingAllState displayingAllState;
        private SearchingState searchingState;
        private IState currentState;

        public bool IsVisible { get; set; }

        public void Inject(
            OptionEntryRepository optionEntryRepository,
            DisplayingAllState displayingAllState,
            SearchingState searchingState)
        {
            this.optionEntryRepository = optionEntryRepository;
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

        public void AddOption(string category, IOption option)
        {
            if (optionEntryRepository.Contains(option))
            {
                throw new InvalidOperationException("Cheat is already registered");
            }

            var optionEntry = new OptionEntry(option, category);
            optionEntryRepository.Add(option, optionEntry);

            currentState.OnOptionAdded(optionEntry);

            OptionEvents.PublishOptionAdded(category, option);
        }

        public void RemoveOption(IOption cheat)
        {
            if (!optionEntryRepository.TryGet(cheat, out var optionEntry))
            {
                throw new InvalidOperationException("Cheat was not registered");
            }

            optionEntryRepository.Remove(cheat);
            currentState.OnOptionRemoved(optionEntry);

            OptionEvents.PublishOptionRemoved(cheat);
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
