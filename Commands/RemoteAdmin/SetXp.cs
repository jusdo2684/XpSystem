using CommandSystem;
using Exiled.API.Features;
using System;
using System.Linq;

namespace XpSystem.Commands.RemoteAdmin
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetXp : ICommand, IUsageProvider
    {
        public string Command => "set";

        public string[] Aliases => new string[0];

        public string Description => "Set the level and exp of a player.";

        public string[] Usage => new[] { "Id", "Lvl", "Exp" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!int.TryParse(arguments.ElementAt(0), out int id))
            {
                response = "Player Id invalid.";
                return false;
            }

            if (!Player.TryGet(id, out Player player))
            {
                response = "Player not found.";
                return false;
            }

            if (int.TryParse(arguments.ElementAt(1), out int lvl))
            {
                response = "Entered Level invalid.";
                return false;
            }

            if (int.TryParse(arguments.ElementAt(2), out int exp))
            {
                response = "Entered Exp invalid.";
                return false;
            }

            if (!PlayerXp.TryGetPlayerXp(player, out PlayerXp playerXp))
            {
                response = "Error : PlayerXp cannot be got.";
                return false;
            }

            playerXp.Level = lvl;
            playerXp.Exp = exp;

            response = player.Nickname + "'s stats have been changed : Level = " + lvl + " / Exp = " + exp + ".";
            return true;
        }
    }
}