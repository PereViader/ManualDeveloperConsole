using UnityEngine;

namespace ManualCheats.Core
{
    public class DropdownButtonCheatRuntimeWidget<T> : ICheatRuntimeWidget
    {
        private readonly DropwdownButtonCheatRuntimeWidgetReferences dropdownButtonCheatWidgetReferences;
        private readonly DropdownButtonCheat<T> dropdownButtonCheat;

        public GameObject GameObject => dropdownButtonCheatWidgetReferences.gameObject;

        public DropdownButtonCheatRuntimeWidget(
            DropwdownButtonCheatRuntimeWidgetReferences dropdownButtonCheatWidgetReferences,
            DropdownButtonCheat<T> dropdownButtonCheat)
        {
            this.dropdownButtonCheatWidgetReferences = dropdownButtonCheatWidgetReferences;
            this.dropdownButtonCheat = dropdownButtonCheat;
        }

        public void Initialize()
        {
            dropdownButtonCheatWidgetReferences.nameText.SetText(dropdownButtonCheat.Name);

            dropdownButtonCheatWidgetReferences.dropdown.options.Clear();
            foreach (var optionName in dropdownButtonCheat.OptionNames)
            {
                dropdownButtonCheatWidgetReferences.dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(optionName));
            }

            dropdownButtonCheatWidgetReferences.button.onClick.AddListener(Button_OnClick);
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        private void Button_OnClick()
        {
            var activeIndex = dropdownButtonCheatWidgetReferences.dropdown.value;

            if (activeIndex < 0)
            {
                return;
            }

            var value = dropdownButtonCheat.OptionValues[activeIndex];

            dropdownButtonCheat.OnActivate(value);
        }
    }
}
