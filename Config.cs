using Exiled.API.Interfaces;

namespace XpSystem
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public int ExpToLvlUp { get; set; } = 5000;

        public int KillExp { get; set; } = 30;
        public int KillScpExp { get; set; } = 50;
        public int EscapeExp { get; set; } = 40;
        public int EatingCandyExp { get; set; } = 15;
        public int ResurrectZombieExp { get; set; } = 20;
        public int ConsumingCorpseExp { get; set; } = 10;
    }
}
