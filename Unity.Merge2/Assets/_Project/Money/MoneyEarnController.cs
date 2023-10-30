using Architecture_Base.Core;
using Assets._Project.Items;
using Assets._Project.Items.Merge;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Money
{
    public class MoneyEarnController : Controller
    {
        private readonly GameConfigLoader _configLoader;
        private readonly Player _player;
        private readonly MoneyUICounter _uiCounter;
        private readonly MergeGrid _mergeGrid;
        private readonly MergeGridController _mergeGridController;
        private readonly ItemBase _itemBase;
        private float _cooldown;
        private GameConfig _config;

        public MoneyEarnController(GameConfigLoader configLoader,
            Player player, MoneyUICounter uiCounter, MergeGrid mergeGrid,
            MergeGridController mergeGridController, ItemBase itemBase) 
        {
            _configLoader = configLoader;
            _player = player;
            _uiCounter = uiCounter;
            _mergeGrid = mergeGrid;
            _mergeGridController = mergeGridController;
            _itemBase = itemBase;
        }

        public override async Task InitializeAsync()
        {
            _config = await _configLoader.LoadAsync();
        }

        public override void Tick()
        {
            if (_cooldown >= 1)
            {
                Earn();
                _cooldown = 0;
                return;
            }

            _cooldown += _config.EarnCooldownSpeed * _player.EarnCooldownSpeedModifire * Time.deltaTime;
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
