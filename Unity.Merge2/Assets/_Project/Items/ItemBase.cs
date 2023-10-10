using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace Assets._Project.Items
{
    public class ItemBase
    {
        private readonly FastRandom _random;
        private List<Sprite> _sprites = new();
        private readonly List<Item> _items = new();

        public IReadOnlyCollection<Sprite> Sprites => _sprites.AsReadOnly();

        public ItemBase(FastRandom random)
        {
            _random = random;
        }

        public async Task InitializeAsync()
        {
            IList<SpriteAtlas> spriteAtlases = await Addressables.LoadAssetsAsync<SpriteAtlas>("item sprite atlas", null).Task;

            foreach (SpriteAtlas atlas in spriteAtlases)
            {
                Sprite[] sprites = new Sprite[atlas.spriteCount];
                atlas.GetSprites(sprites);
                _sprites.AddRange(sprites);
            }
        }

        public Item GetNewBySpriteName(string spriteName)
        {
            Item item = _items
                .Where(item => item.IsInUse == false)
                .FirstOrDefault(item => item.Sprite.name == spriteName);

            if (item == null)
            {
                item = CreateBySpriteName(spriteName);
                _items.Add(item);
            }

            return item;
        }

        public Item GetRandom() => GetNewBySpriteName(_sprites[_random.Range(0, _sprites.Count)].name);

        private Item CreateBySpriteName(string spriteName)
        {
            Sprite sprite = GetSpriteByName(spriteName);
            int dataMergeLevel = _sprites.IndexOf(sprite);
            int dataMergeResultLevel = dataMergeLevel + 1;
            return dataMergeResultLevel < _sprites.Count 
                ? new Item(sprite, _sprites[dataMergeResultLevel]) 
                : null;
        }

        private Sprite GetSpriteByName(string name) => _sprites.SingleOrDefault(sprite =>  sprite.name == name);
    }
}
