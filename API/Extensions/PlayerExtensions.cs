using Exiled.API.Features;
using XpSystem.API.Features;

namespace XpSystem.API.Extensions
{
    public static class PlayerExtensions
    {
        public static PlayerXp GetPlayerXp(this Player player) => PlayerXp.Get(player);
    }
}
