using CommandSystem;
using Exiled.API.Features;
using System;
using System.Linq;

namespace XpSystem.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetXp : ICommand, IUsageProvider
    {
        public string Command => "set";

        public string[] Aliases => new string[0];

        public string Description => "Modifie le level et l'exp d'un joueur";

        public string[] Usage => new[] { "Id", "Lvl", "Exp" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!int.TryParse(arguments.ElementAt(0), out int id))
            {
                response = "L'id n'est pas valide.";
                return false;
            }

            if (!Player.TryGet(id, out Player player))
            {
                response = "Le joueur est introvable.";
                return false;
            }

            if (int.TryParse(arguments.ElementAt(1), out int lvl))
            {
                response = "Le level entré n'est pas valide.";
                return false;
            }

            if (int.TryParse(arguments.ElementAt(2), out int exp))
            {
                response = "L'exp entré n'est pas valide.";
                return false;
            }

            if (!PlayerXp.TryGetPlayerXp(player, out PlayerXp playerXp))
            {
                response = "Erreur : le PlayerXp n'a pas pu être récupéré.";
                return false;
            }

            playerXp.Level = lvl;
            playerXp.Exp = exp;

            response = "Les stats de " + player.Nickname + " ont été modifié : Level = " + lvl + " / Exp = " + exp + ".";
            return true;
        }
    }
}
