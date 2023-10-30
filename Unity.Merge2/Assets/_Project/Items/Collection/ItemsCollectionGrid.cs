using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Project.Items.Collection
{
    public class ItemsCollectionGrid : Popup
    {
        [SerializeField] private ScrollRect _scrollRect;
        private ItemBase _itemBase;
        private List<Image> _icons = new();

        [Inject]
        public void Construct(ItemBase itemBase)
        {
            _itemBase = itemBase;
            //base.Construct(showHidePattern);
        }

        public async Task InitializeAsync(int collectedItemsCount)
        {
            for (int i = 0; i < _itemBase.Sprites.Count; i++)
            {
                Sprite sprite = _itemBase.Sprites.ElementAt(i);
                Image item = new GameObject(sprite.name).AddComponent<Image>();
                item.sprite = sprite;
                item.transform.SetParent(_scrollRect.content, worldPositionStays: false);
                item.color = Color.black;
                _icons.Add(item);
                await Task.Yield();
            }

            UpdateCollection(collectedItemsCount);
        }

        protected override void OnEnable()
        {
            _scrollRect.verticalNormalizedPosition = 1;
            base.OnEnable();
        }

        public void UpdateCollection(int collectedItemsCount)
        {
            for (int i = 0; i < collectedItemsCount; i++)
            {
                _icons[i].color = Color.white;
            }
        }
    }
}