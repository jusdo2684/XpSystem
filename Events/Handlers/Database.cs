using Exiled.Events.Extensions;
using XpSystem.Events.EventArgs;
using static Exiled.Events.Events;

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


        internal static void OnSavingDatabase(SavingDatabaseEventArgs ev) => SavingDatabase.InvokeSafely(ev);

        internal static void OnSavedDatabase() => SavedDatabase.InvokeSafely();

        internal static void OnLoadingDatabase(LoadingDatabaseEventArgs ev) => LoadingDatabase.InvokeSafely(ev);

        internal static void OnLoadedDatabase() => LoadedDatabase.InvokeSafely();
    }
}