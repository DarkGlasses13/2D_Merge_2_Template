using Assets._Project.Upgrade;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnCooldown { get; private set; }
        [field: SerializeField] public float EarnCooldown { get; private set; }
        [field: SerializeField] public float ResourceEventCooldown { get; private set; }
        [field: SerializeField] public float ResourceEventTime { get; private set; }
        [field: SerializeField] public AnimationCurve EarnCurve { get; private set; }
        [field: SerializeField] public float ResourceRate { get; private set; }
        [field: SerializeField] public float ResourceSpeed { get; private set; }

        [SerializeField] private List<StatUpgrader> _statUpgraders;
        public IEnumerable<StatUpgrader> GetStatUpgraders(Player player) => _statUpgraders.Select(upgrader => upgrader.Clone(player));

        public ulong GetEarn(int itemsCount, int mergeLevel) => (ulong)EarnCurve.Evaluate((1.0f / itemsCount) * mergeLevel);
    }
}
