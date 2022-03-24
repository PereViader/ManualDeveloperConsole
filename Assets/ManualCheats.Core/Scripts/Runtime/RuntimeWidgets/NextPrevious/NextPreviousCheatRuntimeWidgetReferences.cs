using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ManualCheats.Core
{
    public class NextPreviousCheatRuntimeWidgetReferences : MonoBehaviour
    {
        public event Action OnUpdate;

        public TMP_Text nameText;
        public TMP_InputField inputField;
        public Button previousButton;
        public Button nextButton;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}
