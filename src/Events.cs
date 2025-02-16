using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

public partial class Plugin
{
    void RegisterEvents()
    {
        RegisterListener<Listeners.OnTick>(OnTick);
        RegisterListener<Listeners.OnServerPrecacheResources>(OnServerPrecacheResources);
    }

    void UnregisterEvents()
    {

        RemoveListener<Listeners.OnTick>(OnTick);
        RemoveListener<Listeners.OnServerPrecacheResources>(OnServerPrecacheResources);
    }

    int Tick { get; set; } = 0;
    void OnTick()
    {
        Tick++;

        if (Tick < Config.TicksForUpdate)
            return;

        Tick = 0;

        foreach (CCSPlayerController player in Utilities.GetPlayers().Where(p => !p.IsBot))
        {
            if (!player.PawnIsAlive || !playerCookies.ContainsKey(player) || !Utils.HasPermission(player))
                continue;

            var absOrgin = player.PlayerPawn.Value!.AbsOrigin!;

            if (Utils.VecCalculateDistance(TrailLastOrigin[player.Slot], absOrgin) <= 5.0f)
                continue;

            Utils.VecCopy(absOrgin, TrailLastOrigin[player.Slot]);

            CreateTrail(player, absOrgin);
        }
    }

    void OnServerPrecacheResources(ResourceManifest manifest)
    {
        foreach (KeyValuePair<string, Trail> trail in Config.Trails)
            manifest.AddResource(trail.Value.File);
    }
}