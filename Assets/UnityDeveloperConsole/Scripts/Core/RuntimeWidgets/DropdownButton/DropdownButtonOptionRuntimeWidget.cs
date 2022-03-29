using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class DropdownButtonOptionRuntimeWidget<T> : IOptionRuntimeWidget
    {
        private readonly DropwdownButtonOptionRuntimeWidgetReferences dropdownButtonOptionWidgetReferences;
        private readonly DropdownButtonOption<T> dropdownButtonOption;

        public GameObject GameObject => dropdownButtonOptionWidgetReferences.gameObject;

        public DropdownButtonOptionRuntimeWidget(
            DropwdownButtonOptionRuntimeWidgetReferences dropdownButtonOptionWidgetReferences,
            DropdownButtonOption<T> dropdownButtonOption)
        {
            this.dropdownButtonOptionWidgetReferences = dropdownButtonOptionWidgetReferences;
            this.dropdownButtonOption = dropdownButtonOption;
        }

        public void Initialize()
        {
            dropdownButtonOptionWidgetReferences.nameText.SetText(dropdownButtonOption.Name);

            dropdownButtonOptionWidgetReferences.dropdown.options.Clear();
            foreach (var optionName in dropdownButtonOption.OptionNames)
            {
                dropdownButtonOptionWidgetReferences.dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(optionName));
            }

            dropdownButtonOptionWidgetReferences.button.onClick.AddListener(Button_OnClick);
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        private void Button_OnClick()
        {
            var activeIndex = dropdownButtonOptionWidgetReferences.dropdown.value;

            if (activeIndex < 0)
            {
                return;
            }

            var value = dropdownButtonOption.OptionValues[activeIndex];

            dropdownButtonOption.OnActivate(value);
        }
    }
}
