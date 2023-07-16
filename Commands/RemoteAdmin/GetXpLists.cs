using CommandSystem;
using System;

namespace XpSystem.Commands.RemoteAdmin
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GetXpLists : ICommand
    {
        public string Command => "getxplist";

        public string[] Aliases => new string[0];

        public string Description => "Get the Xp Lists";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            string xpRegisteredList = "XpsRegistered : \n";
            foreach (PlayerXp plyXp in XpDataSystem.XpsRegistered)
            {
                xpRegisteredList += " - " + plyXp + "\n";
            }

            string dataXpList = "DataXpItems : \n";
            foreach (DataXpItem dataXp in XpDataSystem.DataXpItems)
            {
                dataXpList += " - " + dataXp + "\n";
            }

            response = "Xps Lists : \n" + xpRegisteredList + "\n" + dataXpList;
            return true;
        }
    }
}