using System;
using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class InstantiateAndCallDelegate<TArg, TObject, TReturn>
        where TObject : UnityEngine.Object
    {
        private readonly TObject prefab;
        private readonly Func<TArg, TObject, TReturn> createWidgetDelegate;

        public InstantiateAndCallDelegate(
            TObject prefab,
            Func<TArg, TObject, TReturn> createWidgetDelegate
            )
        {
            this.prefab = prefab;
            this.createWidgetDelegate = createWidgetDelegate;
        }

        public TReturn Create(TArg arg)
        {
            var instance = GameObject.Instantiate(prefab);
            var result = createWidgetDelegate.Invoke(arg, instance);
            return result;
        }
    }
}
