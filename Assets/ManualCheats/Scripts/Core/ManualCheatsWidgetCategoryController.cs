using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ManualCheats.Core
{
    public class ManualCheatsWidgetCategoryController : MonoBehaviour
    {
        public Transform categoryTransform;
        public TMP_Text categoryNameText;

        private readonly List<ICheatRuntimeWidget> cheatWidgets = new List<ICheatRuntimeWidget>();

        public int WidgetCount => cheatWidgets.Count;

        public void Init(string categoryName)
        {
            gameObject.name = categoryName;
            categoryNameText.SetText(categoryName);
        }

        public void Add(ICheatRuntimeWidget cheatWidget)
        {
            cheatWidgets.Add(cheatWidget);
            cheatWidget.GameObject.transform.SetParent(categoryTransform, false);
        }

        public void Remove(ICheatRuntimeWidget cheatWidget)
        {
            if (!cheatWidgets.Remove(cheatWidget))
            {
                throw new InvalidOperationException("Cheat was not found in the category");
            }

            cheatWidget.GameObject.transform.SetParent(null, false);
        }
    }
}
