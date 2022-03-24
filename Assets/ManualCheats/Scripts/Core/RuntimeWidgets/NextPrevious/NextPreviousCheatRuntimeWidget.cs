using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.Core
{
    public class NextPreviousCheatRuntimeWidget<T> : ICheatRuntimeWidget
    {
        private readonly NextPreviousCheat<T> nextPreviousCheat;
        private readonly NextPreviousCheatRuntimeWidgetReferences references;

        private T previousValue;

        public GameObject GameObject => references.gameObject;

        public NextPreviousCheatRuntimeWidget(
            NextPreviousCheat<T> nextPreviousCheat,
            NextPreviousCheatRuntimeWidgetReferences commonNextPreviousCheatWidget
            )
        {
            this.nextPreviousCheat = nextPreviousCheat;
            this.references = commonNextPreviousCheatWidget;
        }

        public void Initialize()
        {
            references.nameText.SetText(nextPreviousCheat.Name);
            previousValue = nextPreviousCheat.GetValue();
            references.inputField.SetTextWithoutNotify(nextPreviousCheat.ConvertValueToString(previousValue));
        }

        public void InputField_OnValueChanged(string stringValue)
        {
            var value = nextPreviousCheat.ConvertStringToValue(stringValue);
            nextPreviousCheat.SetValue(value);
        }

        public void NextButton_OnClick()
        {
            var value = nextPreviousCheat.GetNextValue(previousValue);
            nextPreviousCheat.SetValue(value);
        }

        public void PreviousButton_OnClick()
        {
            var value = nextPreviousCheat.GetPreviousValue(previousValue);
            nextPreviousCheat.SetValue(value);
        }

        public void Update()
        {
            var currentValue = nextPreviousCheat.GetValue();
            if (!EqualityComparer<T>.Default.Equals(previousValue, currentValue))
            {
                previousValue = currentValue;
                references.inputField.SetTextWithoutNotify(nextPreviousCheat.ConvertValueToString(currentValue));
            }
        }

        public void Activate()
        {
            references.OnUpdate += Update;

            references.previousButton.onClick.AddListener(PreviousButton_OnClick);
            references.nextButton.onClick.AddListener(NextButton_OnClick);
            references.inputField.onValueChanged.AddListener(InputField_OnValueChanged);
        }

        public void Deactivate()
        {
            references.OnUpdate -= Update;

            references.previousButton.onClick.RemoveListener(PreviousButton_OnClick);
            references.nextButton.onClick.RemoveListener(NextButton_OnClick);
            references.inputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
        }
    }
}
