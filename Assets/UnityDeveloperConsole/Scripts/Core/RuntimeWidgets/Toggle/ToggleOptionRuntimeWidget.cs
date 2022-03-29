using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDeveloperConsole.Core
{
    public class ToggleOptionRuntimeWidget : MonoBehaviour, IOptionRuntimeWidget
    {
        public Toggle toggle;
        public TMP_Text nameText;

        private ToggleOption toggleOption;

        public GameObject GameObject => gameObject;

        public void Inject(ToggleOption toggleOption)
        {
            this.toggleOption = toggleOption;
        }

        public void Initialize()
        {
            nameText.SetText(toggleOption.Name);
            toggle.onValueChanged.AddListener(Toggle_OnValueChanged);
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        private void Toggle_OnValueChanged(bool arg0)
        {
            toggleOption.SetValue(arg0);
        }

        private void Update()
        {
            var currentValue = toggleOption.GetValue();
            if (toggle.isOn != currentValue)
            {
                toggle.SetIsOnWithoutNotify(currentValue);
            }
        }
    }
}
