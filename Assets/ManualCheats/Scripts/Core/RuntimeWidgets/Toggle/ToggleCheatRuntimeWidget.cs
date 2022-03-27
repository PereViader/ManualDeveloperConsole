using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ManualCheats.Core
{
    public class ToggleCheatRuntimeWidget : MonoBehaviour, ICheatRuntimeWidget
    {
        public Toggle toggle;
        public TMP_Text nameText;

        private ToggleCheat toggleCheat;

        public GameObject GameObject => gameObject;

        public void Inject(ToggleCheat toggleCheat)
        {
            this.toggleCheat = toggleCheat;
        }

        public void Initialize()
        {
            nameText.SetText(toggleCheat.Name);
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
            toggleCheat.SetValue(arg0);
        }

        private void Update()
        {
            var currentValue = toggleCheat.GetValue();
            if (toggle.isOn != currentValue)
            {
                toggle.SetIsOnWithoutNotify(currentValue);
            }
        }
    }
}
