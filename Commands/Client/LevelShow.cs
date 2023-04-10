using CommandSystem;
using Exiled.API.Features;
using System;

namespace XpSystem.Commands.Client
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class LevelShow : ICommand
    {
        public string Command => "level-show";

        public string[] Aliases => new string[0];

        public string Description => "Show your current Xp Stats.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!PlayerXp.TryGetPlayerXp(player, out PlayerXp playerXp))
            {
                response = "Error : Your PlayerXp cannot be got.";
                return false;
            };

            response = $"{player.Nickname} | Level : <color=#0070A1>{playerXp.Level}</color> | Exp : <color=#0070A1>{playerXp.Exp}</color> | Next Level in : <color=#0070A1>{Main.Instance.Config.ExpToLvlUp - playerXp.Exp}</color>";
            return true;
        }
    }
}
