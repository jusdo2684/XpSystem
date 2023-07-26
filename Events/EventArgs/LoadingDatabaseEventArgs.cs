using Exiled.Events.EventArgs.Interfaces;

namespace XpSystem.Events.EventArgs
{
    public class LoadingDatabaseEventArgs : IExiledEvent
    {
        /// <summary>
        /// True : The database will be loaded / False : The database won't be loaded.
        /// </summary>
        public bool IsAllowed { get; set; } = true;
    }
}
