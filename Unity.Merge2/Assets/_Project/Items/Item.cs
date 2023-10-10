using UnityEngine;

namespace Assets._Project.Items
{
    public class Item
    {
        public Sprite Sprite { get; }
        public Sprite MergeResult { get; }
        public bool IsInUse { get; set; }

        public Item(Sprite sprite, Sprite mergeResult, bool willUse = false)
        {
            Sprite = sprite;
            MergeResult = mergeResult;
            IsInUse = willUse;
        }
    }
}
