using UnityEngine;

namespace Assets._Project.Upgrade
{
    [CreateAssetMenu(menuName = "Stat Upgraders/Better Items Upgrader")]
    public class BetterItemsUpgrader : StatUpgrader
    {
        protected override float GetLevelByStat(Player player)
        {
            return 1.0f / Levels * player.SpawnItemMergeLevel;
        }

        protected override void OnUpgrade(Player player)
        {
            player.SpawnItemMergeLevel++;
        }
    }
}
