using Exiled.API.Features;
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

                Log.Info("Level up ! " + Player.Nickname + " passe au niveau " + Level);
            }
        }

        public static void SetMultiplier() => multiplier = DateTime.Now.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday ? 1.5f : 1;

        public void KillPlayerExp()
        {
            Log.Info(Player.Nickname + " is leaving the game. Killing PlayerXp Instance...");
            XpDataSystem.XpsRegistered.Remove(this);
        }

        public override string ToString() => $"{Player.Nickname} - {Level} - {Exp}";
    }
}
