using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.Core
{
    public class TypeAdapterNextPreviousCheatWidget<T, Y> : ICheatWidget<T>
        where T : ICheat
    {
        private readonly BaseNextPreviousCheat<Y> nextPreviousCheat;
        private readonly CommonNextPreviousCheatWidget commonNextPreviousCheatWidget;

        private Y previousValue;

        public GameObject GameObject => commonNextPreviousCheatWidget.gameObject;

        public TypeAdapterNextPreviousCheatWidget(
            BaseNextPreviousCheat<Y> nextPreviousCheat,
            CommonNextPreviousCheatWidget commonNextPreviousCheatWidget
            )
        {
            this.nextPreviousCheat = nextPreviousCheat;
            this.commonNextPreviousCheatWidget = commonNextPreviousCheatWidget;
        }

        public void Init()
        {
            commonNextPreviousCheatWidget.SetName(nextPreviousCheat.Name);
            previousValue = nextPreviousCheat.GetValue();
            commonNextPreviousCheatWidget.SetValueText(nextPreviousCheat.ConvertValueToString(previousValue));
        }

        public void ChangeValueFromInputField(string stringValue)
        {
            var value = nextPreviousCheat.ConvertStringToValue(stringValue);
            nextPreviousCheat.SetValue(value);
        }

        public void ChangeValueFromNext()
        {
            var value = nextPreviousCheat.GetNextValue(previousValue);
            nextPreviousCheat.SetValue(value);
        }

        public void ChangeValueFromPrevious()
        {
            var value = nextPreviousCheat.GetPreviousValue(previousValue);
            nextPreviousCheat.SetValue(value);
        }

        public void Update()
        {
            var currentValue = nextPreviousCheat.GetValue();
            if (!EqualityComparer<Y>.Default.Equals(previousValue, currentValue))
            {
                previousValue = currentValue;
                commonNextPreviousCheatWidget.SetValueText(nextPreviousCheat.ConvertValueToString(currentValue));
            }
        }

        public void Activate()
        {
            commonNextPreviousCheatWidget.OnUpdate += Update;
            commonNextPreviousCheatWidget.OnNextValueRequested += ChangeValueFromNext;
            commonNextPreviousCheatWidget.OnPreviousValueRequested += ChangeValueFromPrevious;
            commonNextPreviousCheatWidget.OnValueChanged += ChangeValueFromInputField;

            commonNextPreviousCheatWidget.Activate();
        }

        public void Deactivate()
        {
            commonNextPreviousCheatWidget.OnUpdate -= Update;
            commonNextPreviousCheatWidget.OnNextValueRequested -= ChangeValueFromNext;
            commonNextPreviousCheatWidget.OnPreviousValueRequested -= ChangeValueFromPrevious;
            commonNextPreviousCheatWidget.OnValueChanged -= ChangeValueFromInputField;

            commonNextPreviousCheatWidget.Deactivate();
        }
    }
}
