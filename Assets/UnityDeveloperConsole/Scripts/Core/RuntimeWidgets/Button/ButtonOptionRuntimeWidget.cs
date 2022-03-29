using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDeveloperConsole.Core
{
    public class ButtonOptionRuntimeWidget : MonoBehaviour, IOptionRuntimeWidget
    {
        public Button button;
        public TMP_Text nameText;

        public ButtonOption Cheat { get; private set; }

        public GameObject GameObject => gameObject;

        public void Inject(ButtonOption cheat)
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
