using UnityEngine;

namespace Assets._Project.Upgrade
{
    [CreateAssetMenu(menuName = "Stat Upgraders/Resource Event Cooldown Upgrader")]
    public class ResourceEventCooldownUpgrader : StatUpgrader
    {
        [SerializeField] private float _value;

        protected override float GetLevelByStat(Player player)
        {
            return (player.ResourceEventCooldownModifire - 1) / _value;
        }

        protected override void OnUpgrade(Player player)
        {
            player.ResourceEventCooldownModifire += _value;
        }
    }
}
