using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class UnityDeveloperConsoleWidgetContainerController : MonoBehaviour
    {
        public Transform widgetContainerTransform;
        public UnityDeveloperConsoleWidgetCategoryController controllerPrefab;

        private readonly Dictionary<string, UnityDeveloperConsoleWidgetCategoryController> categoryControllers = new Dictionary<string, UnityDeveloperConsoleWidgetCategoryController>();

        public void Add(string category, IOptionRuntimeWidget cheatWidget)
        {
            var categoryController = GetOrCreateCategory(category);
            categoryController.Add(cheatWidget);
        }

        private UnityDeveloperConsoleWidgetCategoryController GetOrCreateCategory(string category)
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

        public void Remove(string category, IOptionRuntimeWidget cheatWidget)
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

        private void RemoveController(string category, UnityDeveloperConsoleWidgetCategoryController categoryController)
        {
            categoryControllers.Remove(category);

            GameObject.Destroy(categoryController.gameObject);
        }
    }
}
