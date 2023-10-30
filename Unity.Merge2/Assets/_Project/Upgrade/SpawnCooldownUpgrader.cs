using UnityEngine;

namespace Assets._Project.Upgrade
{
    [CreateAssetMenu(menuName = "Stat Upgraders/Spawn Cooldown Upgrader")]
    public class SpawnCooldownUpgrader : StatUpgrader
    {
        [SerializeField] private float _value;

        public override void Upgrade(Player player)
        {
            player.SpawnCooldownSpeedModifire += _value;
        }

        protected override int GetLevelByStat(Player player)
        {
            return (int)((player.SpawnCooldownSpeedModifire - 1) / _value);
        }
    }
}
