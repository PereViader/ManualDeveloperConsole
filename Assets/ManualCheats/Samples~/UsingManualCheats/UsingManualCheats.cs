using ManualCheats.Core;
using System;
using UnityEngine;

namespace Examples
{
    public class UsingManualCheats : MonoBehaviour
    {
        public ManualCheatsConfiguration configuration;
        public IManualCheatsService manualCheatsService;

        public void Start()
        {
            manualCheatsService = new ManualCheatsServiceBuilder()
                .AddDefaultCheats(configuration)
                .Build(configuration);

            for (int i = 0; i < 150; i++)
            {
                var category = GetRandomCategory();
                var cheat = GetRandomCheat();
                manualCheatsService.AddCheat(category, cheat);
            }
        }

        private ICheat GetRandomCheat()
        {
            switch (UnityEngine.Random.Range(0, 6))
            {
                case 0:
                    return new DropdownButtonCheat<string>("String dropdown", new[] { "1", "Oliva", "Patata" }, new[] { "1", "Oliva", "Patata" }, (x) => Debug.Log(x));
                case 1:
                    return new DropdownButtonCheat<int>("Int dropdown", new[] { 1, 3, 5 }, new[] { "1", "3", "5" }, (x) => Debug.Log(x));
                case 2:
                    {
                        var value = UnityEngine.Random.Range(0, 2) == 0;
                        return new ToggleCheat($"Toggle", () => value, x => value = x);
                    }
                case 3:
                    {
                        var value = UnityEngine.Random.Range(-1000, 10000);
                        return new NextPreviousCheat<int>(
                            $"Int Cheat",
                            x => value = x,
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
                        return new NextPreviousCheat<float>(
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
                        return new ButtonCheat("Button Cheat", () => { });
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

        ICheat testCheat = new ToggleCheat("e", () => true, _ => { });
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                manualCheatsService.ToggleVisibility();
            }
        }
    }
}
