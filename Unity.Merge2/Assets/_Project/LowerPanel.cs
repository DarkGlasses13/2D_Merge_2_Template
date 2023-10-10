using Assets._Project.Items.Collection;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Project
{
    public class LowerPanel : MonoBehaviour
    {
        public event Action OnTabSelected;

        [SerializeField] private Button _itemsCollectionButton;
        private ItemsCollectionGrid _itemsCollectionGrid;

        [Inject]
        public void Construct(ItemsCollectionGrid itemsCollectionGrid)
        {
            _itemsCollectionGrid = itemsCollectionGrid;
        }

        private void OnEnable()
        {
            _itemsCollectionButton.onClick.AddListener(OnItemsCollectionButtonClicked);
        }

        private void OnItemsCollectionButtonClicked()
        {
            OnTabSelected?.Invoke();
            _itemsCollectionGrid.Show();
        }

        private void OnDisable()
        {
            _itemsCollectionButton.onClick.RemoveListener(OnItemsCollectionButtonClicked);
        }
    }
}