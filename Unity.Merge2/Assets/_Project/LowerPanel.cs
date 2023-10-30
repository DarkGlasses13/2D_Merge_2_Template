using Assets._Project.Items.Collection;
using Assets._Project.Upgrade;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Project
{
    public class LowerPanel : MonoBehaviour
    {
        public event Action OnTabSelected;

        [SerializeField] private Button _itemsCollectionButton, _upgradeButton;
        private ItemsCollectionGrid _itemsCollectionGrid;
        private UpgradePopup _upgradePopup;

        [Inject]
        public void Construct(ItemsCollectionGrid itemsCollectionGrid, 
            UpgradePopup upgradePopup)
        {
            _itemsCollectionGrid = itemsCollectionGrid;
            _upgradePopup = upgradePopup;
        }

        private void OnEnable()
        {
            _itemsCollectionButton.onClick.AddListener(OnItemsCollectionButtonClicked);
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        }

        private void OnUpgradeButtonClicked()
        {
            OnTabSelected?.Invoke();
            _upgradePopup?.Show();
        }

        private void OnItemsCollectionButtonClicked()
        {
            OnTabSelected?.Invoke();
            _itemsCollectionGrid?.Show();
        }

        private void OnDisable()
        {
            _itemsCollectionButton.onClick.RemoveListener(OnItemsCollectionButtonClicked);
            _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        }
    }
}