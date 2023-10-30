using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using XpSystem.API.Features;
using XpSystem.Loader;

namespace XpSystem
{
    internal class PlayerHandler
    {
        internal void OnVerified(VerifiedEventArgs ev)
        {
            if (ev.Player.DoNotTrack)
            {
                Log.Info(ev.Player.Nickname + " is a DNT Player !");
                ev.Player.SetName(ev.Player, "DNT - " + ev.Player.Nickname);
                return;
            }

            if (XpDataSystem.XpsRegistered.Exists(x => x.Player.UserId == ev.Player.UserId))
            {
                Log.Info(ev.Player.Nickname + " found in the XpsRegistered. Loading...");

                PlayerXp pXp = XpDataSystem.XpsRegistered.Find(x => x.Player.UserId == ev.Player.UserId);
                pXp.Player = ev.Player;
                pXp.SetXpNickname();

                Log.Info(ev.Player.Nickname + " PlayerXp loaded !");
                return;
            }

            if (XpDataSystem.DataXpItems.Exists(x => x.UserId == ev.Player.UserId))
            {
                Log.Info(ev.Player.Nickname + " found in the DataXpItems. Loading...");

                DataXpItem dataXp = XpDataSystem.DataXpItems.Find(x => x.UserId == ev.Player.UserId);
                PlayerXp pXp = new(ev.Player, dataXp.Lvl, dataXp.Exp);
                pXp.SetXpNickname();

                Log.Info(ev.Player.Nickname + " PlayerXp loaded !");
                return;
            }

            if (!PlayerXp.TryGet(ev.Player, out PlayerXp playerXp))
            {
                Log.Info("OnJoined Info : Aucune instance de PlayerXp trouvée pour " + ev.Player.Nickname + ". Creation...");
                PlayerXp plyXp = new(ev.Player);
                plyXp.SetXpNickname();
                return;
            }
            
            playerXp.SetXpNickname();
        }
    }
}