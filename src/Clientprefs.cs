using Clientprefs.API;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using Microsoft.Extensions.Logging;

public partial class Plugin
{
    public int TrailCookie = 0;
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
            Logger.LogError("[Trails] Fail load ClientprefsApi! | " + ex.Message);
            throw new Exception("[Trails] Fail load ClientprefsApi! | " + ex.Message);
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
            if (!ClientprefsApi.ArePlayerCookiesCached(player)) continue;
            playerCookies[player] = ClientprefsApi.GetPlayerCookie(player, TrailCookie);
        }
    }

    void OnClientprefDatabaseReady()
    {
        if (ClientprefsApi == null) return;

        TrailCookie = ClientprefsApi.RegPlayerCookie("Trail", "Which trail is equiped", CookieAccess.CookieAccess_Public);

        if (TrailCookie == -1)
        {
            Logger.LogError("[Clientprefs-Trails] Failed to register/load Cookie");
            return;
        }
    }

    void OnPlayerCookiesCached(CCSPlayerController player)
    {
        if (ClientprefsApi == null || TrailCookie == -1) return;

        var cookieValue = ClientprefsApi.GetPlayerCookie(player, TrailCookie);

        if (!string.IsNullOrEmpty(cookieValue))
            playerCookies[player] = cookieValue;
    }
}