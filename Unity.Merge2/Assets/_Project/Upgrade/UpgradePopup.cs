using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets._Project.Upgrade
{
    public class UpgradePopup : Popup
    {
        public event Action<string> OnUpgrade;
        [SerializeField] private Transform _content;
        private readonly List<UIProduct> _products = new();
        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            //base.Construct(showHidePattern);
        }

        public async Task InitializeAsync(IEnumerable<StatUpgrader> upgraders)
        {
            foreach (StatUpgrader upgrader in upgraders)
            {
                GameObject instance = await Addressables.InstantiateAsync("Product", _content).Task;
                UIProduct product = instance.GetComponent<UIProduct>();
                product.Title = upgrader.Title;
                product.Price = upgrader.Price.ToString();
                product.Description = upgrader.Description;
                product.Icon = upgrader.Icon;
                _products.Add(product);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _products.ForEach(product => product.OnBuyButtonClick += Upgrade);
        }

        private void Upgrade(string title) => OnUpgrade?.Invoke(title);

        protected override void OnDisable()
        {
            base.OnDisable();
            _products.ForEach(product => product.OnBuyButtonClick -= Upgrade);
        }

        public void SwitchInteraction(string title, bool isInteractable)
        {
            UIProduct product = _products.SingleOrDefault(product => product.Title == title);

            if (product)
                product.IsInteractable = isInteractable;
        }
    }
}
