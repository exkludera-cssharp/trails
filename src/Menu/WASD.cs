using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using WASDSharedAPI;

public static partial class Menu
{
    public static class WASD
    {
        public static IWasdMenuManager WasdManager = new PluginCapability<IWasdMenuManager>("wasdmenu:manager").Get()!;

        public static void Open(CCSPlayerController player)
        {
            IWasdMenu menu = WasdManager.CreateMenu(Localizer["Menu Title"]);

            menu.Add(Localizer["Menu NoTrail"], (player, option) =>
            {
                SelectNone(player);
            });

            foreach (KeyValuePair<string, Trail> trail in Config.Trails)
            {
                menu.Add(trail.Value.Name, (player, option) =>
                {
                    SelectTrail(player, trail);
                });
            }

            WasdManager.OpenMainMenu(player, menu);
        }
    }
}