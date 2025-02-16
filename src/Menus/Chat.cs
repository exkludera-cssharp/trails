using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;

public static partial class Menu
{
    public static class Chat
    {
        public static void Open(CCSPlayerController player)
        {
            ChatMenu menu = new(Localizer["Menu Title"]);

            menu.AddMenuOption(Localizer["Menu NoTrail"], (player, option) =>
            {
                SelectNone(player);
            });

            foreach (KeyValuePair<string, Trail> trail in Config.Trails)
            {
                menu.AddMenuOption(trail.Value.Name, (player, option) =>
                {
                    SelectTrail(player, trail);
                });
            }

            MenuManager.OpenChatMenu(player, menu);
        }
    }
}