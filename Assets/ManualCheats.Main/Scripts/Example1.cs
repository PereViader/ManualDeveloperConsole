using UnityEngine;

namespace ManualCheats.Core
{
    public class Example1 : MonoBehaviour
    {
        public ManualCheatsConfiguration configuration;
        public IManualCheatsService manualCheatsService;

        public void Start()
        {
            manualCheatsService = new ManualCheatsServiceBuilder()
                .AddDefaultCheats(configuration)
                .Build(configuration);

            for (int i = 0; i < 20; i++)
            {
                var value = Random.Range(0, 2) == 0;
                var iString = i.ToString();
                manualCheatsService.AddCheat("Toggles", new ToggleCheat($"Toggle {i}", () => value, x => value = x));
            }

            for (int i = 20; i < 40; i++)
            {
                var iString = i.ToString();
                manualCheatsService.AddCheat("Buttons", new ButtonCheat($"Button {i}", () => Debug.Log("Button pressed" + iString)));
            }

            for (int i = 40; i < 60; i++)
            {
                var iString = i.ToString();
                var value = Random.Range(-1000, 10000);
                manualCheatsService.AddCheat("Ints", new IntNextPreviousCheat(
                    $"Int Cheat {i}",
                    x => value = x,
                    () => value,
                    x => value + 1,
                    x => value - 1
                    ));
            }

            for (int i = 60; i < 80; i++)
            {
                var iString = i.ToString();

                var value = Random.Range(-1000f, 10000f);
                manualCheatsService.AddCheat("Floats", new FloatNextPreviousCheat(
                    $"Int Cheat {i}",
                    x => value = x,
                    () => value,
                    x => value + 1f,
                    x => value - 1f
                    ));
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                manualCheatsService.ToggleVisibility();
            }
        }
    }
}
