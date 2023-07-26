using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace XpSystem.Events.EventArgs
{
    /// <summary>
    /// Event Args for the <see cref="Handlers.Player.AddedExp"/> event.
    /// </summary>
    public class AddedExpEventArgs : IPlayerEvent
    {
        /// <summary>
        /// Get the Player who added exp.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Get the adding exp amount.
        /// </summary>
        public float Amount { get; }

        /// <summary>
        /// Get if the player is going to level up.
        /// </summary>
        public bool LevelUp { get; }


        internal AddedExpEventArgs(Player player, float amount, bool levelUp)
        {
            Player = player;
            Amount = amount;
            LevelUp = levelUp;
        }
    }
}