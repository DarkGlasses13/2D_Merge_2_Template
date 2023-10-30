using Architecture_Base.Core;
using Assets._Project.Items;
using Assets._Project.Items.Collection;
using Assets._Project.Items.Merge;
using Assets._Project.Money;
using Assets._Project.Upgrade;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project
{
    public class GameRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private readonly ItemBase _itemBase;
        private readonly MergeGridController _mergeGridController;
        private readonly ItemSpawnController _itemSpawnController;
        private readonly ItemCollectController _itemCollectController;
        private readonly UpgradeController _upgradeController;
        private readonly MoneyEarnController _moneyEarnController;
        private readonly MoneyUICounter _moneyUICounter;
        private readonly Player _player;

        public GameRunner(ItemBase itemBase, MergeGridController mergeGridController,
            ItemSpawnController itemSpawnController, ItemCollectController itemCollectController,
            UpgradeController upgradeController, MoneyEarnController moneyEarnController,
            MoneyUICounter moneyUICounter, Player player)
        {
            _itemBase = itemBase;
            _mergeGridController = mergeGridController;
            _itemSpawnController = itemSpawnController;
            _itemCollectController = itemCollectController;
            _upgradeController = upgradeController;
            _moneyEarnController = moneyEarnController;
            _moneyUICounter = moneyUICounter;
            _player = player;
        }

        public void Initialize() => RunAsync();

        protected override async Task CreateControllers()
        {
            await _itemBase.InitializeAsync();
            // Load saved data

            _controllers = new IController[]
            {
                _mergeGridController,
                _itemSpawnController,
                _itemCollectController,
                _upgradeController,
                _moneyEarnController,
            };
        }

        protected override void OnControllersInitialized()
        {
        }

        protected override void OnControllersEnabled()
        {
            _moneyUICounter.Set(_player.Money);
        }

        public void Dispose()
        {
        }
    }
}
