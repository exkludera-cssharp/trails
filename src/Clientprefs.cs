using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using Microsoft.Extensions.Logging;
using Clientprefs.API;

public partial class Plugin
{
    public int TrailCookie = 0;
    public int HideTrailsCookie = 0;
    public Dictionary<CCSPlayerController, string> playerCookies = new();
    public readonly PluginCapability<IClientprefsApi> g_PluginCapability = new("Clientprefs");
    public IClientprefsApi? ClientprefsApi;

    void LoadClientprefs()
    {
        try
        {
            ClientprefsApi = g_PluginCapability.Get();

            if (ClientprefsApi == null) return;
            ClientprefsApi.OnDatabaseLoaded += OnClientprefDatabaseReady;
            ClientprefsApi.OnPlayerCookiesCached += OnPlayerCookiesCached;
        }
        catch (Exception ex)
        {
            Logger.LogError("Fail load ClientprefsApi! | " + ex.Message);
            throw new Exception("Fail load ClientprefsApi! | " + ex.Message);
        }
    }

    void UnloadClientprefs()
    {
        if (ClientprefsApi == null) return;
        ClientprefsApi.OnDatabaseLoaded -= OnClientprefDatabaseReady;
        ClientprefsApi.OnPlayerCookiesCached -= OnPlayerCookiesCached;
    }

    void ReloadClientprefs()
    {
        if (ClientprefsApi == null || TrailCookie == -1) return;

        foreach (CCSPlayerController player in Utilities.GetPlayers().Where(p => !p.IsBot))
        {
            if (!ClientprefsApi.ArePlayerCookiesCached(player))
                continue;

            playerCookies[player] = ClientprefsApi.GetPlayerCookie(player, TrailCookie);

            bool hidetrailsCookie = ClientprefsApi.GetPlayerCookie(player, HideTrailsCookie) == "1";
            if (hidetrailsCookie && !HideTrails.Contains(player))
                HideTrails.Add(player);
        }
    }

    void OnClientprefDatabaseReady()
    {
        if (ClientprefsApi == null) return;

        TrailCookie = ClientprefsApi.RegPlayerCookie("Trail", "Which trail is equiped", CookieAccess.CookieAccess_Public);
        HideTrailsCookie = ClientprefsApi.RegPlayerCookie("HideTrails", "Hide other player's trails", CookieAccess.CookieAccess_Public);

        if (TrailCookie == -1 || HideTrailsCookie == -1)
        {
            Logger.LogError("Failed to register cookies");
            return;
        }
    }

    void OnPlayerCookiesCached(CCSPlayerController player)
    {
        if (ClientprefsApi == null || TrailCookie == -1) return;

        var trailCookie = ClientprefsApi.GetPlayerCookie(player, TrailCookie);

        if (!string.IsNullOrEmpty(trailCookie))
            playerCookies[player] = trailCookie;

        bool hidetrailsCookie = ClientprefsApi.GetPlayerCookie(player, HideTrailsCookie) == "1";
        if (hidetrailsCookie && !HideTrails.Contains(player))
            HideTrails.Add(player);
    }
}