using Exiled.Events.EventArgs.Interfaces;

namespace XpSystem.Events.EventArgs
{
    public class SavingDatabaseEventArgs : IExiledEvent
    {
        /// <summary>
        /// True : The database will be saved / False : The database won't be saved.
        /// </summary>
        public bool IsAllowed { get; set; } = true;
    }
}
