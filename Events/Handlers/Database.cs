using XpSystem.Events.EventArgs;
using Exiled.Events.Features;

namespace XpSystem.Events.Handlers
{
    public class Database
    {
        /// <summary>
        /// Called before saving the xp database.
        /// </summary>
        public static event CustomEventHandler<SavingDatabaseEventArgs> SavingDatabase;
        
        /// <summary>
        /// Called after saving the xp database.
        /// </summary>
        public static event CustomEventHandler SavedDatabase;

        /// <summary>
        /// Called before loading the xp database.
        /// </summary>
        public static event CustomEventHandler<LoadingDatabaseEventArgs> LoadingDatabase;

        /// <summary>
        /// Called after loading the xp database.
        /// </summary>
        public static event CustomEventHandler LoadedDatabase;


        internal static void OnSavingDatabase(SavingDatabaseEventArgs ev) => SavingDatabase.Invoke(ev);

        internal static void OnSavedDatabase() => SavedDatabase.Invoke();

        internal static void OnLoadingDatabase(LoadingDatabaseEventArgs ev) => LoadingDatabase.Invoke(ev);

        internal static void OnLoadedDatabase() => LoadedDatabase.Invoke();
    }
}