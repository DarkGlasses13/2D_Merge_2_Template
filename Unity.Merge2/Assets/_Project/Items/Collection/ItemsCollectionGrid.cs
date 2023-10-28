using Architecture_Base.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Project.Items.Collection
{
    public class ItemsCollectionGrid : MonoBehaviour, IUIElement
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private ScrollRect _scrollRect;
        private ItemBase _itemBase;
        private List<Image> _icons = new();

        [Inject]
        public void Construct(ItemBase itemBase)
        {
            _itemBase = itemBase;
        }

        public async Task InitializeAsync(int collectedItemsCount)
        {
            for (int i = 0; i < _itemBase.Sprites.Count; i++)
            {
                Sprite sprite = _itemBase.Sprites.ElementAt(i);
                Image item = new GameObject(sprite.name).AddComponent<Image>();
                item.sprite = sprite;
                item.transform.SetParent(_scrollRect.content, worldPositionStays: false);
                item.color = i >= collectedItemsCount ? Color.black : Color.white;
                _icons.Add(item);
                await Task.Yield();
            }
        }

        public void Show(Action callback = null)
        {
            if (gameObject.activeInHierarchy)
                return;

            _scrollRect.verticalNormalizedPosition = 1;
            gameObject.SetActive(true);
            callback?.Invoke();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked() => Hide();

        public void Hide(Action callback = null)
        {
            gameObject.SetActive(false);
            callback?.Invoke();
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void UpdateCollection(int collectedItemsCount)
        {
            for (int i = 0; i <= collectedItemsCount; i++)
            {
                _icons[i].color = Color.white;
            }
        }
    }
}