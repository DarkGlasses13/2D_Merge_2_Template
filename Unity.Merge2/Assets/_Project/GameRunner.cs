using Architecture_Base.Core;
using Assets._Project.Items;
using Assets._Project.Items.Collection;
using Assets._Project.Items.Merge;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project
{
    public class GameRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private readonly ItemBase _itemBase;
        private readonly ItemsCollectionGrid _itemsCollectionGrid;
        private readonly MergeGridController _mergeGridController;
        private readonly ItemSpawnController _itemSpawnController;
        private readonly ItemCollectController _itemCollectController;

        public GameRunner(ItemBase itemBase, ItemsCollectionGrid itemsCollectionGrid,
            MergeGridController mergeGridController, ItemSpawnController itemSpawnController,
            ItemCollectController itemCollectController)
        {
            _itemBase = itemBase;
            _itemsCollectionGrid = itemsCollectionGrid;
            _mergeGridController = mergeGridController;
            _itemSpawnController = itemSpawnController;
            _itemCollectController = itemCollectController;
        }

        public void Initialize() => RunAsync();

        protected override async Task CreateControllers()
        {
            await _itemBase.InitializeAsync();

            _controllers = new IController[]
            {
                _mergeGridController,
                _itemSpawnController,
                _itemCollectController,
            };
        }

        protected override void OnControllersInitialized()
        {
        }

        protected override void OnControllersEnabled()
        {
        }

        public void Dispose()
        {
        }
    }
}
