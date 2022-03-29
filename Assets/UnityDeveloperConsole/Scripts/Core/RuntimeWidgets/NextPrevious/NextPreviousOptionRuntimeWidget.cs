using System.Collections.Generic;
using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class NextPreviousOptionRuntimeWidget<T> : IOptionRuntimeWidget
    {
        private readonly NextPreviousOption<T> nextPreviousOption;
        private readonly NextPreviousOptionRuntimeWidgetReferences references;

        private T previousValue;

        public GameObject GameObject => references.gameObject;

        public NextPreviousOptionRuntimeWidget(
            NextPreviousOption<T> nextPreviousOption,
            NextPreviousOptionRuntimeWidgetReferences commonNextPreviousOptionWidget
            )
        {
            this.nextPreviousOption = nextPreviousOption;
            this.references = commonNextPreviousOptionWidget;
        }

        public void Initialize()
        {
            references.nameText.SetText(nextPreviousOption.Name);
            previousValue = nextPreviousOption.GetValue();
            references.inputField.SetTextWithoutNotify(nextPreviousOption.ConvertValueToString(previousValue));

            references.OnUpdate += Update;

            references.previousButton.onClick.AddListener(PreviousButton_OnClick);
            references.nextButton.onClick.AddListener(NextButton_OnClick);
            references.inputField.onValueChanged.AddListener(InputField_OnValueChanged);
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public void InputField_OnValueChanged(string stringValue)
        {
            var value = nextPreviousOption.ConvertStringToValue(stringValue);
            nextPreviousOption.SetValue(value);
        }

        public void NextButton_OnClick()
        {
            var value = nextPreviousOption.GetNextValue(previousValue);
            nextPreviousOption.SetValue(value);
        }

        public void PreviousButton_OnClick()
        {
            var value = nextPreviousOption.GetPreviousValue(previousValue);
            nextPreviousOption.SetValue(value);
        }

        public void Update()
        {
            var currentValue = nextPreviousOption.GetValue();
            if (!EqualityComparer<T>.Default.Equals(previousValue, currentValue))
            {
                previousValue = currentValue;
                references.inputField.SetTextWithoutNotify(nextPreviousOption.ConvertValueToString(currentValue));
            }
        }
    }
}
