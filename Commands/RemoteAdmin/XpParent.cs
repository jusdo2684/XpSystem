using CommandSystem;
using System;

namespace XpSystem.Commands.RemoteAdmin
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class XpParent : ParentCommand
    {
        public XpParent() => LoadGeneratedCommands();

        public override string Command => "xp";

        public override string[] Aliases => new string[0];

        public override string Description => "Manage the Xp";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new SetXp());
            RegisterCommand(new ResetXp());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 0)
            {
                response = "Done !";
                return false;
            }

            response = "Voici la liste des sous commandes : \n - set : modifier le level et l'exp d'un joueur \n - reset : réinitialiser l'exp d'un joueur";
            return true;
        }
    }
}
