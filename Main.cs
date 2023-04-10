using Exiled.API.Features;
using System;
using XpSystem.EventHandlers;

namespace XpSystem
{
    public class Main : Plugin<Config>
    {
        public override string Name => "Xp System";

        public override string Author => "JusDo2684";

        public override Version Version => new(1, 0, 0);

        readonly PlayerHandler plyHandler = new();
        readonly ScpHandler scpHandler = new();

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
            Exiled.Events.Handlers.Player.Dying += plyHandler.OnDying;
            Exiled.Events.Handlers.Player.Escaping += plyHandler.OnEscaping;
            Exiled.Events.Handlers.Scp330.EatingScp330 += scpHandler.OnEatingScp330;
            Exiled.Events.Handlers.Scp049.FinishingRecall += scpHandler.OnRessurectZombie;
            Exiled.Events.Handlers.Scp049.ConsumingCorpse += scpHandler.OnConsumingCorpse;
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
            Exiled.Events.Handlers.Player.Dying -= plyHandler.OnDying;
            Exiled.Events.Handlers.Player.Escaping -= plyHandler.OnEscaping;
            Exiled.Events.Handlers.Scp330.EatingScp330 -= scpHandler.OnEatingScp330;
            Exiled.Events.Handlers.Scp049.FinishingRecall -= scpHandler.OnRessurectZombie;
            Exiled.Events.Handlers.Scp049.ConsumingCorpse -= scpHandler.OnConsumingCorpse;

            Instance = null;
        }
    }
}
