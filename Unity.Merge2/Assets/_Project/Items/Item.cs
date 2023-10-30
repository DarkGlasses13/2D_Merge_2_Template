using UnityEngine;

namespace Assets._Project.Items
{
    public class Item
    {
        public Sprite Sprite { get; }
        public int MergeLevel { get; }
        public Sprite MergeResult { get; }
        public bool IsInUse { get; set; }

        public Item(Sprite sprite, int mergeLevel, Sprite mergeResult, bool willUse = false)
        {
            Sprite = sprite;
            MergeLevel = mergeLevel;
            MergeResult = mergeResult;
            IsInUse = willUse;
        }
    }
}
