using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Translations;

public partial class Plugin : BasePlugin, IPluginConfig<Config>
{
    public override string ModuleName => "Trails";
    public override string ModuleVersion => "1.1.0";
    public override string ModuleAuthor => "exkludera";

    public static Plugin Instance { get; set; } = new();

    public override void Load(bool hotReload)
    {
        Instance = this;

        RegisterEvents();

        foreach (var command in Config.MenuCommands)
            AddCommand($"css_{command}", "", Menu.Open);

        foreach (var command in Config.HideTrailsCommands)
            AddCommand($"css_{command}", "", Command_HideTrails);

        for (int i = 0; i < 64; i++)
        {
            TrailEndOrigin[i] = new();
            TrailLastOrigin[i] = new();
        }
    }

    public override void Unload(bool hotReload)
    {
        UnregisterEvents();

        foreach (var command in Config.MenuCommands)
            RemoveCommand($"css_{command}", Menu.Open);

        foreach (var command in Config.HideTrailsCommands)
            RemoveCommand($"css_{command}", Command_HideTrails);

        UnloadClientprefs();
    }

    public override void OnAllPluginsLoaded(bool hotReload)
    {
        LoadClientprefs();

        if (hotReload)
            ReloadClientprefs();
    }

    public Config Config { get; set; } = new Config();
    public void OnConfigParsed(Config config)
    {
        Config = config;
        Config.Prefix = StringExtensions.ReplaceColorTags(config.Prefix);
    }
}
