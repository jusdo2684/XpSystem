using Exiled.API.Interfaces;
using System;
using System.ComponentModel;
using System.IO;

namespace XpSystem
{
    internal class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool Debug { get; set; } = false;


        [Description("Exp amount required to level-up.")]
        public int ExpToLvlUp { get; set; } = 5000;
        
        [Description("Exp earned when a player kills another player.")]
        public int KillExp { get; set; } = 30;

        [Description("Exp earned when a human kills an SCP.")]
        public int KillScpExp { get; set; } = 50;

        [Description("Exp earned when a player escapes the facility.")]
        public int EscapeExp { get; set; } = 40;

        [Description("Exp earned when a player eats a SCP-330 candy.")]
        public int EatingCandyExp { get; set; } = 15;

        [Description("Exp earned when a SCP-049 ressurects a player.")]
        public int ResurrectZombieExp { get; set; } = 20;

        [Description("Exp earned when a zombie consumes a dead body.")]
        public int ConsumingCorpseExp { get; set; } = 10;


        [Description("The folder path for the database. Warning : Don't put the database file name (automatic)")]
        public string DatabaseDirectoryPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "/EXILED/Configs/XpSystem");

        [Description("The name of the json file of the database")]
        public string DatabaseFileName { get; set; } = "Database";
    }
}