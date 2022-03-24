namespace ManualCheats.Core
{
    public static class ManualCheatsServiceExtensions
    {
        public static void ToggleVisibility(this IManualCheatsService manualCheatsService)
        {
            if (manualCheatsService.IsVisible)
            {
                manualCheatsService.Hide();
            }
            else
            {
                manualCheatsService.Show();
            }
        }
    }
}
