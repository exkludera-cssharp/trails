using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using CS2MenuManager.API.Class;
using CS2MenuManager.API.Interface;
using CS2MenuManager.API.Enum;
using Clientprefs.API;

public static partial class Menu
{
    private static Plugin Instance = Plugin.Instance;
    private static IStringLocalizer Localizer = Instance.Localizer;
    private static int TrailCookie = Instance.TrailCookie;
    private static IClientprefsApi? ClientprefsApi = Instance.ClientprefsApi;

    [CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
    public static void Open(CCSPlayerController? player, CommandInfo info)
    {
        if (player == null) return;

        if (!Utils.HasPermission(player))
        {
            Utils.PrintToChat(player, Localizer["No Permission"]);
            return;
        }

        IMenu Menu = MenuManager.MenuByType(Instance.Config.MenuType, Localizer["Menu Title"], Instance);

        bool isHidden = Plugin.HideTrails.Contains(player);
        Menu.AddItem(isHidden ? Localizer["Menu ShowTrails"] : Localizer["Menu HideTrails"], (player, option) =>
        {
            if (isHidden)
            {
                Plugin.HideTrails.Remove(player);
                Utils.PrintToChat(player, Localizer["Trails Shown"]);
            }
            else
            {
                Plugin.HideTrails.Add(player);
                Utils.PrintToChat(player, Localizer["Trails Hidden"]);
            }

            Open(player, info);
        });

        Menu.AddItem(Localizer["Menu NoTrail"], (player, option) =>
        {
            if (ClientprefsApi == null || TrailCookie == -1)
                return;

            ClientprefsApi.SetPlayerCookie(player, TrailCookie, "none");
            Instance.playerCookies[player] = "none";

            Utils.PrintToChat(player, Localizer["Trail Remove"]);

            Open(player, info);
        }, Instance.playerCookies[player].Equals("none") ? DisableOption.DisableShowNumber : DisableOption.None);

        foreach (KeyValuePair<string, Trail> trail in Instance.Config.Trails)
        {
            Menu.AddItem(trail.Value.Name, (player, option) =>
            {
                if (ClientprefsApi == null || TrailCookie == -1)
                    return;

                ClientprefsApi.SetPlayerCookie(player, TrailCookie, trail.Key);
                Instance.playerCookies[player] = trail.Key;

                Utils.PrintToChat(player, Localizer["Trail Select", trail.Value.Name]);

                Open(player, info);
            });
        }

        Menu.ExitButton = true;
        Menu.Display(player, 0);
    }
}