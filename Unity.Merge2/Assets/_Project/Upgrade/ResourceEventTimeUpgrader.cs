using UnityEngine;

namespace Assets._Project.Upgrade
{
    [CreateAssetMenu(menuName = "Stat Upgraders/Resource Event Time Upgrader")]
    public class ResourceEventTimeUpgrader : StatUpgrader
    {
        [SerializeField] private float _value;

        protected override float GetLevelByStat(Player player)
        {
            return (player.ResourceEventTimeModifire - 1) / _value;
        }

        protected override void OnUpgrade(Player player)
        {
            player.ResourceEventTimeModifire += _value;
        }
    }
}
