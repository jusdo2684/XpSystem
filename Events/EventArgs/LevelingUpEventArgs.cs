using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace XpSystem.Events.EventArgs
{
    /// <summary>
    /// Event Args for the <see cref="Handlers.Player.LevelingUp"/> event.
    /// </summary>
    public class LevelingUpEventArgs : IPlayerEvent
    {
        /// <summary>
        /// Get the player leveling up.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Get his old level.
        /// </summary>
        public int OldLevel { get; }

        /// <summary>
        /// Get his new level.
        /// </summary>
        public int NewLevel { get; }

        /// <summary>
        /// True : PLayer level up / False : Player doesn't level up
        /// </summary>
        public bool IsAllowed { get; set; }


        internal LevelingUpEventArgs(Player player, int oldLevel, int newLevel)
        {
            Player = player;
            OldLevel = oldLevel;
            NewLevel = newLevel;
            IsAllowed = true;
        }
    }
}