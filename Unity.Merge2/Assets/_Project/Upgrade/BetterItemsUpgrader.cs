using UnityEngine;

namespace Assets._Project.Upgrade
{
    [CreateAssetMenu(menuName = "Stat Upgraders/Better Items Upgrader")]
    public class BetterItemsUpgrader : StatUpgrader
    {
        public override void Upgrade(Player player)
        {
            player.SpawnItemMergeLevel++;
        }

        protected override int GetLevelByStat(Player player)
        {
            return 1 / Levels * player.SpawnItemMergeLevel;
        }
    }
}
