using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ManualCheats.Core
{
    public class ButtonCheatRuntimeWidget : MonoBehaviour, ICheatRuntimeWidget
    {
        public Button button;
        public TMP_Text nameText;

        public ButtonCheat Cheat { get; private set; }

        public GameObject GameObject => gameObject;

        public void Inject(ButtonCheat cheat)
        {
            Cheat = cheat;
        }

        public void Initialize()
        {
            nameText.text = Cheat.Name;
            button.onClick.AddListener(Button_OnClick);
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public void Button_OnClick()
        {
            Cheat.Action?.Invoke();
        }
    }
}
