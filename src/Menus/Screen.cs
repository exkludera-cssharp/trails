using CounterStrikeSharp.API.Core;
using CS2ScreenMenuAPI;

public static partial class Menu
{
    public static class Screen
    {
        public static void Open(CCSPlayerController player)
        {
            ScreenMenu menu = new ScreenMenu(Localizer["Menu Title"], Instance);

            menu.AddOption(Localizer["Menu NoTrail"], (player, option) =>
            {
                SelectNone(player);
            });

            foreach (KeyValuePair<string, Trail> trail in Config.Trails)
            {
                menu.AddOption(trail.Value.Name, (player, option) =>
                {
                    SelectTrail(player, trail);
                });
            }

            MenuAPI.OpenMenu(Instance, player, menu);
        }
    }
}