using Exiled.Events.Features;
using XpSystem.Events.EventArgs;

namespace XpSystem.Events.Handlers
{
    public class Player
    {
        /// <summary>
        /// Called before adding exp.
        /// </summary>
        public static event CustomEventHandler<AddingExpEventArgs> AddingExp;

        /// <summary>
        /// Called after adding exp.
        /// </summary>
        public static event CustomEventHandler<AddedExpEventArgs> AddedExp;

        /// <summary>
        /// Called before leveling up.
        /// </summary>
        public static event CustomEventHandler<LevelingUpEventArgs> LevelingUp;

        /// <summary>
        /// Called after leveling up.
        /// </summary>
        public static event CustomEventHandler<LeveledUpEventArgs> LeveledUp;


        internal static void OnAddingExp(AddingExpEventArgs ev) => AddingExp.Invoke(ev);

        internal static void OnAddedExp(AddedExpEventArgs ev) => AddedExp.Invoke(ev);

        internal static void OnLevelingUp(LevelingUpEventArgs ev) => LevelingUp.Invoke(ev);

        internal static void OnLeveledUp(LeveledUpEventArgs ev) => LeveledUp.Invoke(ev);
    }
}