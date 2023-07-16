using Exiled.API.Features;
using System;

namespace XpSystem
{
    public class Main : Plugin<Config>
    {
        public override string Name => "Xp System";

        public override string Author => "JusDo2684";

        public override Version Version => new(0, 1);

        readonly PlayerHandler plyHandler = new();

        public static Main Instance { get; private set; }

        public override void OnEnabled()
        {
            base.OnEnabled();

            Instance = this;

            Exiled.Events.Handlers.Server.WaitingForPlayers += PlayerXp.SetMultiplier;
            Exiled.Events.Handlers.Server.WaitingForPlayers += XpDataSystem.LoadDatabase;
            Exiled.Events.Handlers.Server.ReloadedPlugins += XpDataSystem.SaveDatabase;
            Exiled.Events.Handlers.Server.ReloadedPlugins += XpDataSystem.LoadDatabase;
            Exiled.Events.Handlers.Server.RestartingRound += XpDataSystem.SaveDatabase;

            Exiled.Events.Handlers.Player.Verified += plyHandler.OnVerified;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            XpDataSystem.SaveDatabase();

            Exiled.Events.Handlers.Server.WaitingForPlayers -= PlayerXp.SetMultiplier;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= XpDataSystem.LoadDatabase;
            Exiled.Events.Handlers.Server.ReloadedPlugins -= XpDataSystem.SaveDatabase;
            Exiled.Events.Handlers.Server.ReloadedPlugins -= XpDataSystem.LoadDatabase;
            Exiled.Events.Handlers.Server.RestartingRound -= XpDataSystem.SaveDatabase;

            Exiled.Events.Handlers.Player.Verified -= plyHandler.OnVerified;

            Instance = null;
        }
    }
}