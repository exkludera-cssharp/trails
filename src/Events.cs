using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

public partial class Plugin
{
    private void RegisterEvents()
    {
        RegisterListener<Listeners.OnTick>(OnTick);
        RegisterListener<Listeners.OnServerPrecacheResources>(OnServerPrecacheResources);
        RegisterListener<Listeners.CheckTransmit>(CheckTransmit);
    }

    private void UnregisterEvents()
    {

        RemoveListener<Listeners.OnTick>(OnTick);
        RemoveListener<Listeners.OnServerPrecacheResources>(OnServerPrecacheResources);
        RemoveListener<Listeners.CheckTransmit>(CheckTransmit);
    }

    private int Tick { get; set; } = 0;
    private void OnTick()
    {
        Tick++;

        if (Tick < Config.TicksForUpdate)
            return;

        Tick = 0;

        foreach (CCSPlayerController player in Utilities.GetPlayers().Where(p => !p.IsBot && p.PawnIsAlive))
        {
            if (!Utils.HasPermission(player) || !playerCookies.ContainsKey(player))
                continue;

            var absOrgin = player.PlayerPawn.Value?.AbsOrigin!;

            if (Utils.VecCalculateDistance(TrailLastOrigin[player.Slot], absOrgin) <= 5.0f)
                continue;

            Utils.VecCopy(absOrgin, TrailLastOrigin[player.Slot]);

            CreateTrail(player, absOrgin);
        }
    }

    private void OnServerPrecacheResources(ResourceManifest manifest)
    {
        foreach (KeyValuePair<string, Trail> trail in Config.Trails)
            manifest.AddResource(trail.Value.File);
    }

    private void CheckTransmit(CCheckTransmitInfoList infoList)
    {
        foreach ((CCheckTransmitInfo info, CCSPlayerController? player) in infoList)
        {
            if (player == null || player.IsBot)
                continue;

            if (!HideTrails.Contains(player))
                continue;

            foreach ((CEntityInstance? entity, CCSPlayerController? owner) in TrailsList)
            {
                if (owner != player)
                    info.TransmitEntities.Remove(entity);
            }
        }
    }
}