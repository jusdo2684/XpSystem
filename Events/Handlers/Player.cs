using Exiled.Events.Extensions;
using XpSystem.Events.EventArgs;
using static Exiled.Events.Events;

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


        internal static void OnAddingExp(AddingExpEventArgs ev) => AddingExp.InvokeSafely(ev);

        internal static void OnAddedExp(AddedExpEventArgs ev) => AddedExp.InvokeSafely(ev);

        internal static void OnLevelingUp(LevelingUpEventArgs ev) => LevelingUp.InvokeSafely(ev);

        internal static void OnLeveledUp(LeveledUpEventArgs ev) => LeveledUp.InvokeSafely(ev);
    }
}