using CommandSystem;
using Exiled.API.Features;
using System;
using System.Linq;

namespace XpSystem.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ResetXp : ICommand, IUsageProvider
    {
        public string Command => "reset";

        public string[] Aliases => new string[0];

        public string Description => "Reset the level and exp of a player";

        public string[] Usage => new[] { "Id" };

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

            if (!PlayerXp.TryGetPlayerXp(player, out PlayerXp playerXp))
            {
                response = "Error : Player cannot be got.";
                return false;
            }

            playerXp.Level = 0;
            playerXp.Exp = 0;

            response = player.Nickname + "'s stats have been reset.";
            return true;
        }
    }
}