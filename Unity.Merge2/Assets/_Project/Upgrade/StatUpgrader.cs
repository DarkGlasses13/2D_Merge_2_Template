using UnityEngine;

namespace Assets._Project.Upgrade
{
    public abstract class StatUpgrader : ScriptableObject
    {
        private float _level;
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [SerializeField] private AnimationCurve _priceCurve;
        [field: SerializeField] public int Levels { get; private set; }
        public float Price => _priceCurve.Evaluate(Level);
        public float Level { get => _level; set => _level = Mathf.Clamp01(value); }
        public bool IsDone => Level == 1;

        public StatUpgrader Clone(Player player)
        {
            StatUpgrader clone = (StatUpgrader)MemberwiseClone();
            clone.Level = GetLevelByStat(player);
            return clone;
        }

        public void Upgrade(Player player)
        {
            Level += 1.0f / Levels;
            OnUpgrade(player);
        }

        protected abstract void OnUpgrade(Player player);
        protected abstract float GetLevelByStat(Player player);
    }
}
