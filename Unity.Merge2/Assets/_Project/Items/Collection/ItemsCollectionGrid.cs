using Architecture_Base.UI;
using System;
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

        [Inject]
        public void Construct(ItemBase itemBase)
        {
            _itemBase = itemBase;
        }

        public async Task InitializeAsync()
        {
            foreach (Sprite sprite in _itemBase.Sprites)
            {
                Image item = new GameObject(sprite.name).AddComponent<Image>();
                item.sprite = sprite;
                item.transform.SetParent(_scrollRect.content, worldPositionStays: false);
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
    }
}