using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace XpSystem.Events.EventArgs
{
    /// <summary>
    /// Event Args for the <see cref="Handlers.Player.LeveledUp"/> event.
    /// </summary>
    public class LeveledUpEventArgs : IPlayerEvent
    {
        /// <summary>
        /// Get the Player who leveled up.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Get the Player level.
        /// </summary>
        public int Level { get; }


        internal LeveledUpEventArgs(Player player, int level)
        {
            Player = player;
            Level = level;
        }
    }
}