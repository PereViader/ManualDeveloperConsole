using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ManualCheats.Core
{
    public class CommonNextPreviousCheatWidget : MonoBehaviour
    {
        public event Action OnUpdate;
        public event Action<string> OnValueChanged;
        public event Action OnNextValueRequested;
        public event Action OnPreviousValueRequested;

        public TMP_Text nameText;
        public TMP_InputField inputField;
        public Button previousButton;
        public Button nextButton;

        public GameObject GameObject => gameObject;

        public void Activate()
        {
            previousButton.onClick.AddListener(PreviousButton_OnClick);
            nextButton.onClick.AddListener(NextButton_OnClick);
            inputField.onValueChanged.AddListener(InputField_OnValueChanged);
        }

        public void Deactivate()
        {
            previousButton.onClick.RemoveListener(PreviousButton_OnClick);
            nextButton.onClick.RemoveListener(NextButton_OnClick);
            inputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
        }

        public void SetName(string value)
        {
            nameText.SetText(value);
        }

        public void SetValueText(string value)
        {
            inputField.SetTextWithoutNotify(value);
        }

        private void InputField_OnValueChanged(string arg0)
        {
            OnValueChanged?.Invoke(arg0);
        }

        private void NextButton_OnClick()
        {
            OnNextValueRequested?.Invoke();
        }

        private void PreviousButton_OnClick()
        {
            OnPreviousValueRequested?.Invoke();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}
