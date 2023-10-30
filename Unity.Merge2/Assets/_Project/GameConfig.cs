using Assets._Project.Upgrade;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float CooldownSpeed { get; private set; }
        [SerializeField] private List<StatUpgrader> _statUpgraders;
        public IEnumerable<StatUpgrader> GetStatUpgraders(Player player) => _statUpgraders.Select(upgrader => upgrader.Clone(player));
    }
}
