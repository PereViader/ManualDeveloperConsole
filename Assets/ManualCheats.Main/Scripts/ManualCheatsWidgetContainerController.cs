using System;
using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.Core
{
    public class ManualCheatsWidgetContainerController : MonoBehaviour
    {
        public Transform widgetContainerTransform;
        public ManualCheatsWidgetCategoryController controllerPrefab;

        private readonly Dictionary<string, ManualCheatsWidgetCategoryController> categoryControllers = new Dictionary<string, ManualCheatsWidgetCategoryController>();

        public void Add(string category, ICheatWidget cheatWidget)
        {
            var categoryController = GetOrCreateCategory(category);
            categoryController.Add(cheatWidget);
        }

        private ManualCheatsWidgetCategoryController GetOrCreateCategory(string category)
        {
            if (categoryControllers.TryGetValue(category, out var categoryController))
            {
                return categoryController;
            }

            categoryController = GameObject.Instantiate(controllerPrefab, widgetContainerTransform, false);
            categoryController.Init(category);
            categoryControllers.Add(category, categoryController);
            return categoryController;
        }

        public void Remove(string category, ICheatWidget cheatWidget)
        {
            if (!categoryControllers.TryGetValue(category, out var categoryController))
            {
                throw new InvalidOperationException($"Category controller was not found for {category}");
            }

            categoryController.Remove(cheatWidget);

            if (categoryController.WidgetCount == 0)
            {
                RemoveController(category, categoryController);
            }
        }

        private void RemoveController(string category, ManualCheatsWidgetCategoryController categoryController)
        {
            categoryControllers.Remove(category);

            GameObject.Destroy(categoryController);
        }
    }
}
