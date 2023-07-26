using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp049;
using Exiled.Events.EventArgs.Scp330;
using XpSystem.Events.EventArgs;
using XpSystem.Loader;

namespace XpSystem.API.Features
{
    public class PlayerXp
    {
        /// <summary>
        /// <see cref="Exiled.API.Features.Player"/> instance for the Xp.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// The level of the <see cref="Player"/>.
        /// </summary>
        public int Level { get; set; } = 0;

        /// <summary>
        /// The exp of the <see cref="Player"/>.
        /// </summary>
        public float Exp { get; set; } = 0;

        /// <summary>
        /// Create a clear instance of <see cref="PlayerXp"/>.
        /// </summary>
        /// <param name="player"></param>
        public PlayerXp(Player player)
        {
            Player = player;
            XpDataSystem.XpsRegistered.Add(this);
            SetXpNickname();

            Exiled.Events.Handlers.Player.Dying += OnDying;
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
            Exiled.Events.Handlers.Scp049.FinishingRecall += OnRessurectZombie;
            Exiled.Events.Handlers.Scp049.ConsumingCorpse += OnConsumingCorpse;
            Exiled.Events.Handlers.Scp330.EatingScp330 += OnEatingScp330;

            Log.Info("New Instance of PlayerXp for " + Player.Nickname);
        }

        /// <summary>
        /// Create a instance of <see cref="PlayerXp"/> filled with data.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="level"></param>
        /// <param name="exp"></param>
        public PlayerXp(Player player, int level, float exp)
        {
            Player = player;
            Level = level;
            Exp = exp;

            XpDataSystem.XpsRegistered.Add(this);

            Log.Info(Player.Nickname + " PlayerXp loaded from XpDataItems to XpsRegistered.");
        }

        internal void SetXpNickname() => Player.CustomName = $"Lvl {Level} - {Player.Nickname}";

        public static PlayerXp Get(Player player) => XpDataSystem.XpsRegistered.Find(x => x.Player == player);

        public static bool TryGet(Player player, out PlayerXp playerXp)
        {
            playerXp = XpDataSystem.XpsRegistered.Find(x => x.Player == player);
            return playerXp is not null;
        }

        public static void GetAndAddExp(Player player, int amount) => XpDataSystem.XpsRegistered.Find(x => x.Player == player).AddExp(amount);

        public void AddExp(float amount)
        {
            AddingExpEventArgs addingEv = new(Player, amount);
            Events.Handlers.Player.OnAddingExp(addingEv);
            if (!addingEv.IsAllowed) return;

            Exp += amount;

            Log.Info(Player.Nickname + " earned " + amount + " xp");

            AddedExpEventArgs addedEv = new(Player, amount, Exp >= Main.Instance.Config.ExpToLvlUp);
            Events.Handlers.Player.OnAddedExp(addedEv);

            if (Exp >= Main.Instance.Config.ExpToLvlUp)
            {
                LevelingUpEventArgs lvlingEv = new(Player, Level, Level++);
                Events.Handlers.Player.OnLevelingUp(lvlingEv);
                if (!lvlingEv.IsAllowed) return;

                Level++;
                Exp = 0;
                SetXpNickname();

                Player.PlayBeepSound();
                Player.ShowHint($"Level Unlock : <color=#0070A1>{Level}</color>");

                Log.Info("Level up ! " + Player.Nickname + " passe au niveau " + Level);

                LeveledUpEventArgs lvledEv = new(Player, Level);
                Events.Handlers.Player.OnLeveledUp(lvledEv);
            }
        }


        internal void KillPlayerExp()
        {
            Exiled.Events.Handlers.Player.Dying -= OnDying;
            Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
            Exiled.Events.Handlers.Scp049.FinishingRecall -= OnRessurectZombie;
            Exiled.Events.Handlers.Scp049.ConsumingCorpse -= OnConsumingCorpse;
            Exiled.Events.Handlers.Scp330.EatingScp330 -= OnEatingScp330;

            Log.Info(Player.Nickname + " is leaving the game. Killing PlayerXp Instance...");
            XpDataSystem.XpsRegistered.Remove(this);
        }

        public override string ToString() => $"{Player.Nickname} - {Level} - {Exp}";


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