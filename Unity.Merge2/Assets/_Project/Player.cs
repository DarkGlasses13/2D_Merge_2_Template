namespace Assets._Project
{
    public class Player
    {
        public ulong Money { get; set; }
        public float EarnCooldownSpeedModifire { get; set; } = 1;
        public int CollectedItemsCount { get; set; }
        public float SpawnCooldownSpeedModifire { get; set; } = 1;
        public int SpawnItemMergeLevel { get; set; }

        public void Update(Player player)
        {

        }
    }
}
