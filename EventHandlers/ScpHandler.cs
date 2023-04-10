using Exiled.Events.EventArgs.Scp049;
using Exiled.Events.EventArgs.Scp330;

namespace XpSystem.EventHandlers
{
    public class ScpHandler
    {
        public void OnEatingScp330(EatingScp330EventArgs ev) => PlayerXp.GetAndAddExp(ev.Player, Main.Instance.Config.EatingCandyExp);
        public void OnRessurectZombie(FinishingRecallEventArgs ev) => PlayerXp.GetAndAddExp(ev.Player, Main.Instance.Config.ResurrectZombieExp);
        public void OnConsumingCorpse(ConsumingCorpseEventArgs ev) => PlayerXp.GetAndAddExp(ev.Player, Main.Instance.Config.ConsumingCorpseExp);
    }
}
