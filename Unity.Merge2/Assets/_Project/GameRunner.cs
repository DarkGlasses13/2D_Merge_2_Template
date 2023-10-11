﻿using Architecture_Base.Core;
using Assets._Project.Items;
using Assets._Project.Items.Collection;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project
{
    public class GameRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private readonly ItemBase _itemBase;
        private readonly ItemsCollectionGrid _itemsCollectionGrid;
        private readonly ItemSpawnController _itemSpawnController;

        public GameRunner(ItemBase itemBase, ItemsCollectionGrid itemsCollectionGrid,
            ItemSpawnController itemSpawnController)
        {
            _itemBase = itemBase;
            _itemsCollectionGrid = itemsCollectionGrid;
            _itemSpawnController = itemSpawnController;
        }

        public void Initialize() => RunAsync();

        protected override async Task CreateControllers()
        {
            await _itemBase.InitializeAsync();
            await _itemsCollectionGrid.InitializeAsync();

            _controllers = new IController[]
            {
                //_itemSpawnController,
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