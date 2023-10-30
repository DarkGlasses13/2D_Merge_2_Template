using Assets._Project.Money;
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
        private readonly Dictionary<string, UIProduct> _products = new();
        private Player _player;
        private MoneyFormater _formater;

        [Inject]
        public void Construct(Player player, MoneyFormater moneyFormater)
        {
            _player = player;
            _formater = moneyFormater;
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
                _products.Add(upgrader.Title, product);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            foreach (var product in _products.Values)
                product.OnBuyButtonClick += Upgrade;
        }

        private void Upgrade(string title) => OnUpgrade?.Invoke(title);

        protected override void OnDisable()
        {
            base.OnDisable();

            foreach (var product in _products.Values)
                product.OnBuyButtonClick -= Upgrade;
        }

        public void UpdateData(string title, float price, bool isInteractable)
        {
            if (_products.ContainsKey(title))
            {
                _products[title].Price = _formater.Format((ulong)price);
                _products[title].IsInteractable = isInteractable;
            }
        }
    }
}
