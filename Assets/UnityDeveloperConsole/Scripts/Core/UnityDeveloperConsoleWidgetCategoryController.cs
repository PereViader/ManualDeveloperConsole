using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class UnityDeveloperConsoleWidgetCategoryController : MonoBehaviour
    {
        public Transform categoryTransform;
        public TMP_Text categoryNameText;

        private readonly List<IOptionRuntimeWidget> optionRuntimeWidgets = new List<IOptionRuntimeWidget>();

        public int WidgetCount => optionRuntimeWidgets.Count;

        public void Init(string categoryName)
        {
            gameObject.name = categoryName;
            categoryNameText.SetText(categoryName);
        }

        public void Add(IOptionRuntimeWidget optionRuntimeWidget)
        {
            optionRuntimeWidgets.Add(optionRuntimeWidget);
            optionRuntimeWidget.GameObject.transform.SetParent(categoryTransform, false);
        }

        public void Remove(IOptionRuntimeWidget optionRuntimeWidget)
        {
            if (!optionRuntimeWidgets.Remove(optionRuntimeWidget))
            {
                throw new InvalidOperationException("Cheat was not found in the category");
            }

            optionRuntimeWidget.GameObject.transform.SetParent(null, false);
        }
    }
}
