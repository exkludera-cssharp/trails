using Clientprefs.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CS2MenuManager.API.Class;
using CS2MenuManager.API.Interface;
using Microsoft.Extensions.Localization;

public static partial class Menu
{
    static Plugin Instance = Plugin.Instance;
    static Config Config = Instance.Config;
    static IStringLocalizer Localizer = Instance.Localizer;
    static int TrailCookie = Instance.TrailCookie;
    static Dictionary<CCSPlayerController, string> playerCookies = Instance.playerCookies;
    static IClientprefsApi? ClientprefsApi = Instance.ClientprefsApi;

    [CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
    public static void Open(CCSPlayerController? player, CommandInfo info)
    {
        if (player == null) return;

        if (!Utils.HasPermission(player))
        {
            Utils.PrintToChat(player, Localizer["No Permission"]);
            return;
        }

        IMenu Menu = MenuManager.MenuByType(Config.MenuType, Localizer["Menu Title"], Instance);

        Menu.AddItem(Localizer["Menu NoTrail"], (player, option) =>
        {
            SelectNone(player);
        });

        foreach (KeyValuePair<string, Trail> trail in Config.Trails)
        {
            Menu.AddItem(trail.Value.Name, (player, option) =>
            {
                SelectTrail(player, trail);
            });
        }

        Menu.Display(player, 0);
    }

    public static void SelectNone(CCSPlayerController player)
    {
        if (ClientprefsApi == null || TrailCookie == -1)
            return;

        ClientprefsApi.SetPlayerCookie(player, TrailCookie, "none");
        playerCookies[player] = "none";

        Utils.PrintToChat(player, Localizer["Trail Remove"]);
    }

    public static void SelectTrail(CCSPlayerController player, KeyValuePair<string, Trail> trail)
    {
        if (ClientprefsApi == null || TrailCookie == -1)
            return;

        ClientprefsApi.SetPlayerCookie(player, TrailCookie, trail.Key);
        playerCookies[player] = trail.Key;

        Utils.PrintToChat(player, Localizer["Trail Select", trail.Value.Name]);
    }
}