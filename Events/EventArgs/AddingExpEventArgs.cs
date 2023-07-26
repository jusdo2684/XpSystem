using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace XpSystem.Events.EventArgs
{
    /// <summary>
    /// Event Args for the <see cref="Handlers.Player.AddingExp"/> event.
    /// </summary>
    public class AddingExpEventArgs : IPlayerEvent
    {
        /// <summary>
        /// Get the Player adding exp.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Get the amount of exp adding.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// True : The Player adds exp / False : The Player doesn't add exp.
        /// </summary>
        public bool IsAllowed { get; set; }


        internal AddingExpEventArgs(Player player, float amount)
        {
            Player = player;
            Amount = amount;
            IsAllowed = true;
        }
    }
}