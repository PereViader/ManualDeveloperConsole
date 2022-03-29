using System;
using UnityDeveloperConsole.Core;
using UnityEngine;

namespace Examples
{
    public class SampleUsage : MonoBehaviour
    {
        public UnityDeveloperConsoleRuntimeConfiguration configuration;
        public IUnityDeveloperConsole unityDeveloperConsole;

        public void Start()
        {
            unityDeveloperConsole = new UnityDeveloperConsoleBuilder()
                .AddDefaultOptions(configuration)
                .Build(configuration);

            unityDeveloperConsole.AddOption(
                "Demonstration",
                new ButtonOption("Add random cheat", () =>
                {
                    unityDeveloperConsole.AddOption(
                        GetRandomCategory(),
                        GetRandomOption()
                        );
                }));
        }

        private IOption GetRandomOption()
        {
            switch (UnityEngine.Random.Range(0, 6))
            {
                case 0:
                    return new DropdownButtonOption<string>("String dropdown", new[] { "1", "Oliva", "Patata" }, new[] { "1", "Oliva", "Patata" }, (x) => Debug.Log(x));
                case 1:
                    return new DropdownButtonOption<int>("Int dropdown", new[] { 1, 3, 5 }, new[] { "1", "3", "5" }, (x) => Debug.Log(x));
                case 2:
                    {
                        var value = UnityEngine.Random.Range(0, 2) == 0;
                        return new ToggleOption(
                            $"Toggle",
                            () => value,
                            x =>
                            {
                                Debug.Log("Set value to " + x);
                                value = x;
                            });
                    }
                case 3:
                    {
                        var value = UnityEngine.Random.Range(-1000, 10000);
                        return new NextPreviousOption<int>(
                            $"Int Cheat",
                            x =>
                            {
                                Debug.Log("Set value to " + x);
                                value = x;
                            },
                            () => value,
                            x => value + 1,
                            x => value - 1,
                            int.Parse,
                            Convert.ToString
                            );
                    }
                case 4:
                    {
                        var value = UnityEngine.Random.Range(-1000f, 10000f);
                        return new NextPreviousOption<float>(
                            $"Float Cheat",
                            x => value = x,
                            () => value,
                            x => value + 1f,
                            x => value - 1f,
                            float.Parse,
                            Convert.ToString
                            );
                    }
                case 5:
                    {
                        return new ButtonOption("Button Option", () => Debug.Log("Button was pressed"));
                    }
                default:
                    throw new System.Exception();
            }
        }

        private string GetRandomCategory()
        {
            switch (UnityEngine.Random.Range(0, 5))
            {
                case 0:
                    return "Game Setup";
                case 1:
                    return "User Configuration";
                case 2:
                    return "Battle";
                case 3:
                    return "Debug";
                case 4:
                    return "Configuration";
            }

            return "No category";
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                unityDeveloperConsole.ToggleVisibility();
            }
        }
    }
}
