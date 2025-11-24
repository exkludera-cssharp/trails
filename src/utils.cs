using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Utils;
using System.Drawing;

public static class Utils
{
    private static Plugin Instance = Plugin.Instance;
    private static Config Config = Instance.Config;

    public static void PrintToChat(CCSPlayerController player, string Message)
    {
        if (Config.ChatMessages)
            player.PrintToChat(Config.Prefix + Message);
    }

    public static bool HasPermission(CCSPlayerController player)
    {
        if (Config.Permission.Count == 0)
            return true;

        foreach (string perm in Config.Permission)
        {
            if (perm.StartsWith("@") && AdminManager.PlayerHasPermissions(player, perm))
                return true;
            if (perm.StartsWith("#") && AdminManager.PlayerInGroup(player, perm))
                return true;
        }
        return false;
    }

    public static float VecCalculateDistance(Vector vector1, Vector vector2)
    {
        float dx = vector2.X - vector1.X;
        float dy = vector2.Y - vector1.Y;
        float dz = vector2.Z - vector1.Z;

        return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
    public static void VecCopy(Vector vector1, Vector vector2)
    {
        vector2.X = vector1.X;
        vector2.Y = vector1.Y;
        vector2.Z = vector1.Z;
    }
    public static bool VecIsZero(Vector vector)
    {
        return vector.LengthSqr() == 0;
    }

    private static int colorIndex = 0;
    private static readonly Color[] RainbowColors = GenerateSmoothRainbow(64);
    private static Color[] GenerateSmoothRainbow(int steps)
    {
        var colors = new Color[steps];
        for (int i = 0; i < steps; i++)
        {
            float hue = (float)i / steps;
            colors[i] = HslToColor(hue, 1.0f, 0.5f);
        }
        return colors;
    }
    public static Color GetNextRainbowColor()
    {
        var color = RainbowColors[colorIndex];
        colorIndex = (colorIndex + 1) % RainbowColors.Length;
        return color;
    }

    private static Color HslToColor(float h, float s, float l)
    {
        float r, g, b;

        if (s == 0f)
        {
            r = g = b = l;
        }
        else
        {
            float q = l < 0.5f ? l * (1 + s) : l + s - l * s;
            float p = 2 * l - q;
            r = HueToRgb(p, q, h + 1f / 3f);
            g = HueToRgb(p, q, h);
            b = HueToRgb(p, q, h - 1f / 3f);
        }

        return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
    }

    private static float HueToRgb(float p, float q, float t)
    {
        if (t < 0f) t += 1f;
        if (t > 1f) t -= 1f;
        if (t < 1f / 6f) return p + (q - p) * 6f * t;
        if (t < 1f / 2f) return q;
        if (t < 2f / 3f) return p + (q - p) * (2f / 3f - t) * 6f;
        return p;
    }
}