using Architecture_Base.Core;
using Assets._Project.Items;
using Assets._Project.Items.Merge;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Money
{
    public class MoneyEarnController : Controller
    {
        private readonly GameConfigLoader _configLoader;
        private readonly Player _player;
        private readonly UICounter _uiCounter;
        private readonly MergeGrid _mergeGrid;
        private readonly MergeGridController _mergeGridController;
        private readonly ItemBase _itemBase;
        private readonly ResourceEventController _resourceController;
        private float _cooldown;
        private GameConfig _config;

        public MoneyEarnController(GameConfigLoader configLoader,
            Player player, MoneyUICounter uiCounter, MergeGrid mergeGrid,
            MergeGridController mergeGridController, ItemBase itemBase,
            ResourceEventController resourceController) 
        {
            _configLoader = configLoader;
            _player = player;
            _uiCounter = uiCounter;
            _mergeGrid = mergeGrid;
            _mergeGridController = mergeGridController;
            _itemBase = itemBase;
            _resourceController = resourceController;
        }

        public override async Task InitializeAsync()
        {
            _config = await _configLoader.LoadAsync();
        }

        public override void Tick()
        {
            if (_resourceController.IsEventActive)
                return;

            if (_cooldown >= _config.EarnCooldown / _player.EarnCooldownModifire)
            {
                Earn();
                _cooldown = 0;
                return;
            }

            _cooldown += Time.deltaTime;
        }

        private void Earn()
        {
            ulong summ = 0;

            foreach (Item item in _mergeGrid.Items)
            {
                if (item != null)
                    summ += _config.GetEarn(_itemBase.Count, item.MergeLevel);
            }

            _player.Money += summ;
            _uiCounter.Set(_player.Money);
            _mergeGridController.OnEarn();
        }
    }
}
