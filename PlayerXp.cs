using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp049;
using Exiled.Events.EventArgs.Scp330;
using System;

namespace XpSystem
{
    public class PlayerXp
    {
        static float multiplier = 1;

        public Player Player { get; set; }

        public int Level { get; set; } = 0;

        public int Exp { get; set; } = 0;


        public PlayerXp(Player player)
        {
            Player = player;
            XpDataSystem.XpsRegistered.Add(this);
            SetXpNickname();

            Exiled.Events.Handlers.Player.Dying += OnDying;
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
            Exiled.Events.Handlers.Player.Joined += OnJoined;
            Exiled.Events.Handlers.Scp049.FinishingRecall += OnRessurectZombie;
            Exiled.Events.Handlers.Scp049.ConsumingCorpse += OnConsumingCorpse;
            Exiled.Events.Handlers.Scp330.EatingScp330 += OnEatingScp330;

            Log.Info("New Instance of PlayerXp for " + Player.Nickname);
        }

        public PlayerXp(Player player, int level, int exp)
        {
            Player = player;
            Level = level;
            Exp = exp;

            XpDataSystem.XpsRegistered.Add(this);

            Log.Info(Player.Nickname + " PlayerXp loaded from XpDataItems to XpsRegistered.");
        }

        public void SetXpNickname() => Player.CustomName = $"Lvl {Level} - {Player.Nickname}";

        public static PlayerXp GetPlayerXp(Player player) => XpDataSystem.XpsRegistered.Find(x => x.Player == player);

        public static bool TryGetPlayerXp(Player player, out PlayerXp playerXp)
        {
            playerXp = XpDataSystem.XpsRegistered.Find(x => x.Player == player);
            return playerXp is not null;
        }

        public static void GetAndAddExp(Player player, int amount) => XpDataSystem.XpsRegistered.Find(x => x.Player == player).AddExp(amount);

        public void AddExp(float amount)
        {
            amount *= multiplier;
            Exp += (int)amount;

            Log.Info(Player.Nickname + " earned " + amount + " xp");

            if (Exp >= Main.Instance.Config.ExpToLvlUp)
            {
                Level++;
                Exp = 0;
                SetXpNickname();

                Player.PlayBeepSound();
                Player.ShowHint($"Level Unlock : <color=#0070A1>{Level}</color>");

                Log.Info("Level up ! " + Player.Nickname + " passe au niveau " + Level);
            }
        }

        public static void SetMultiplier() => multiplier = DateTime.Now.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday ? 1.5f : 1;

        public void KillPlayerExp()
        {
            Exiled.Events.Handlers.Player.Dying -= OnDying;
            Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
            Exiled.Events.Handlers.Player.Joined -= OnJoined;
            Exiled.Events.Handlers.Scp049.FinishingRecall -= OnRessurectZombie;
            Exiled.Events.Handlers.Scp049.ConsumingCorpse -= OnConsumingCorpse;
            Exiled.Events.Handlers.Scp330.EatingScp330 -= OnEatingScp330;

            Log.Info(Player.Nickname + " is leaving the game. Killing PlayerXp Instance...");
            XpDataSystem.XpsRegistered.Remove(this);
        }

        public override string ToString() => $"{Player.Nickname} - {Level} - {Exp}";

        void OnJoined(JoinedEventArgs ev) => ev.Player.ShowHint($"Exp Synchronization :  \nLevel : <color=#0070A1>{Level}</color> | Exp : <color=#0070A1>{Exp}</color> | Next Level in : <color=#0070A1>{Main.Instance.Config.ExpToLvlUp - Exp}</color>");

        void OnDying(DyingEventArgs ev)
        {
            if (ev.Attacker == ev.Player || ev.Attacker is null || ev.Player is null) return;
            AddExp(ev.Player.IsScp ? Main.Instance.Config.KillScpExp : Main.Instance.Config.KillExp);
        }
        void OnEscaping(EscapingEventArgs _) => AddExp(Main.Instance.Config.EscapeExp);

        void OnEatingScp330(EatingScp330EventArgs _) => AddExp(Main.Instance.Config.EatingCandyExp);
        void OnRessurectZombie(FinishingRecallEventArgs _) => AddExp(Main.Instance.Config.ResurrectZombieExp);
        void OnConsumingCorpse(ConsumingCorpseEventArgs _) => AddExp(Main.Instance.Config.ConsumingCorpseExp);
    }
}
