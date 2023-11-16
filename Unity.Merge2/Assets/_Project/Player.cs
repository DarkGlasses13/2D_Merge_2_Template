namespace Assets._Project
{
    public class Player
    {
        public ulong Money { get; set; }
        public ulong Resource { get; set; }
        public int CollectedItemsCount { get; set; }
        public int SpawnItemMergeLevel { get; set; }
        public float SpawnCooldownModifire { get; set; } = 1;
        public float EarnCooldownModifire { get; set; } = 1;
        public float ResourceEventTimeModifire { get; set; } = 1;
        public float ResourceEventCooldownModifire { get; set; } = 1;

        public void Update(Player player)
        {

        }
    }
}
