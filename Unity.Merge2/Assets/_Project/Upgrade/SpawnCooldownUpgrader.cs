using UnityEngine;

namespace Assets._Project.Upgrade
{
    [CreateAssetMenu(menuName = "Stat Upgraders/Spawn Cooldown Upgrader")]
    public class SpawnCooldownUpgrader : StatUpgrader
    {
        [SerializeField] private float _value;

        protected override float GetLevelByStat(Player player)
        {
            return (player.SpawnCooldownSpeedModifire - 1) / _value;
        }

        protected override void OnUpgrade(Player player)
        {
            player.SpawnCooldownSpeedModifire += _value;
        }
    }
}
