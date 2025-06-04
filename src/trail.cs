using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using System.Drawing;

public partial class Plugin
{
    static readonly Vector[] TrailLastOrigin = new Vector[64];
    static readonly Vector[] TrailEndOrigin = new Vector[64];

    public void CreateTrail(CCSPlayerController player, Vector absOrigin)
    {
        if (player != null && playerCookies.TryGetValue(player, out var cookieValue))
        {
            if (string.IsNullOrEmpty(cookieValue) || cookieValue == "none")
                return;

            if (Config.Trails.TryGetValue(cookieValue, out var trailData))
            {
                if (trailData.File.EndsWith(".vpcf"))
                    CreateParticle(player, absOrigin, trailData);

                else CreateBeam(player, absOrigin, trailData);
            }
        }
    }

    public void CreateParticle(CCSPlayerController player, Vector absOrigin, Trail trailData)
    {
        float lifetimeValue = trailData.Lifetime > 0 ? trailData.Lifetime : 1.0f;
     
        var particle = Utilities.CreateEntityByName<CEnvParticleGlow>("env_particle_glow")!;

        particle.EffectName = trailData.File;
        particle.StartActive = true;
        particle.Teleport(absOrigin);
        particle.DispatchSpawn();
        particle.AcceptInput("FollowEntity", player.PlayerPawn?.Value!, player.PlayerPawn?.Value!, "!activator");

        AddTimer(lifetimeValue, () =>
        {
            if (particle != null && particle.IsValid)
                particle.Remove();
        });
    }

    public void CreateBeam(CCSPlayerController player, Vector absOrigin, Trail trailData)
    {
        string colorValue = !string.IsNullOrEmpty(trailData.Color) ? trailData.Color : "255 255 255";
        float widthValue = trailData.Width > 0 ? trailData.Width : 1.0f;
        float lifetimeValue = trailData.Lifetime > 0 ? trailData.Lifetime : 1.0f;
        string fileValue = !string.IsNullOrEmpty(trailData.File) ? trailData.File : "materials/sprites/laserbeam.vtex";

        Color color = Color.FromArgb(255, 255, 255, 255);
        if (string.IsNullOrEmpty(colorValue) || colorValue == "rainbow")
        {
            color = Utils.rainbowColors[Utils.colorIndex];
            Utils.colorIndex = (Utils.colorIndex + 1) % Utils.rainbowColors.Length;
        }
        else
        {
            var colorParts = colorValue.Split(' ');
            if (colorParts.Length == 3 &&
                int.TryParse(colorParts[0], out var r) &&
                int.TryParse(colorParts[1], out var g) &&
                int.TryParse(colorParts[2], out var b)
            )
            color = Color.FromArgb(255, r, g, b);
        }

        if (Utils.VecIsZero(TrailEndOrigin[player.Slot]))
        {
            Utils.VecCopy(absOrigin, TrailEndOrigin[player.Slot]);
            return;
        }

        var beam = Utilities.CreateEntityByName<CBeam>("env_beam")!;

        beam.Width = widthValue;
        beam.Render = color;
        beam.SetModel(fileValue);
        beam.Teleport(absOrigin);
        beam.DispatchSpawn();

        Utils.VecCopy(TrailEndOrigin[player.Slot], beam.EndPos);
        Utils.VecCopy(absOrigin, TrailEndOrigin[player.Slot]);

        Utilities.SetStateChanged(beam, "CBeam", "m_vecEndPos");

        AddTimer(lifetimeValue, () =>
        {
            if (beam != null && beam.DesignerName == "env_beam")
                beam.Remove();
        });
    }
}